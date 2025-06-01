using EXE202_BE.Data.Models;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Net.payOS;
using Net.payOS.Types;
using System.Threading.Tasks;
using EXE202_BE.Data.DTOS.PayOS;
using EXE202_BE.Data;
using System;
using Microsoft.EntityFrameworkCore;

namespace EXE202_BE.Service.Services;

public class PayOSService
{
    private readonly PayOS _payOS;
    private readonly string _returnUrl;
    private readonly string _cancelUrl;
    private readonly ILogger<PayOSService> _logger;
    private readonly AppDbContext _dbContext;

    public PayOSService(IConfiguration config, ILogger<PayOSService> logger, AppDbContext dbContext)
    {
        _logger = logger;
        _dbContext = dbContext;

        var payosCredPath = Environment.GetEnvironmentVariable("PAYOS_CREDENTIALS");
        if (string.IsNullOrEmpty(payosCredPath))
        {
            throw new ArgumentNullException("PAYOS_CREDENTIALS", "Environment variable PAYOS_CREDENTIALS is not set.");
        }

        string payosCredJson;
        if (File.Exists(payosCredPath))
        {
            payosCredJson = File.ReadAllText(payosCredPath);
            _logger.LogInformation($"Loaded PayOS credentials from file: {payosCredPath}");
        }
        else
        {
            throw new FileNotFoundException($"PayOS credentials file not found at: {payosCredPath}");
        }

        var payosCredentials = JsonSerializer.Deserialize<PayOSCredentials>(payosCredJson);
        if (payosCredentials == null)
        {
            throw new Exception("Failed to parse PayOS credentials file.");
        }

        _payOS = new PayOS(payosCredentials.ClientId, payosCredentials.ApiKey, payosCredentials.ChecksumKey);

        _returnUrl = config["PayOSSettings:ReturnUrl"];
        _cancelUrl = config["PayOSSettings:CancelUrl"];
        _logger.LogInformation("PayOSService initialized with ReturnUrl: {ReturnUrl}, CancelUrl: {CancelUrl}", _returnUrl, _cancelUrl);
    }

public async Task<PaymentResponse> CreatePaymentLink(PaymentRequest request, int upId)
    {
        try
        {
            _logger.LogInformation($"Creating payment link for order: {request.OrderCode}, amount: {request.Amount}, upId: {upId}");

            // Tạo orderCode tăng dần
            var lastTransaction = await _dbContext.PaymentTransactions
                .OrderByDescending(t => t.OrderCode)
                .FirstOrDefaultAsync();
            long newOrderCode = lastTransaction != null ? lastTransaction.OrderCode + 1 : 1;
            request.OrderCode = newOrderCode;

            var items = request.Items ?? new List<ItemData>();
            var paymentData = new PaymentData(
                orderCode: request.OrderCode,
                amount: request.Amount,
                description: request.Description,
                items: items,
                returnUrl: _returnUrl,
                cancelUrl: _cancelUrl,
                buyerName: request.BuyerName,
                buyerEmail: request.BuyerEmail,
                buyerPhone: request.BuyerPhone,
                expiredAt: (int)(DateTime.UtcNow.AddDays(7) - new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).TotalSeconds // Hết hạn sau 7 ngày
            );

            _logger.LogDebug($"Sending payment data to PayOS: {JsonSerializer.Serialize(paymentData)}");

            CreatePaymentResult createPaymentResult = await _payOS.createPaymentLink(paymentData);
            _logger.LogDebug($"PayOS response: {JsonSerializer.Serialize(createPaymentResult)}");

            if (createPaymentResult.status != "PENDING")
            {
                _logger.LogError($"PayOS returned error - Status: {createPaymentResult.status}, Description: {createPaymentResult.description}");
                throw new Exception($"PayOS error - Status: {createPaymentResult.status}, Description: {createPaymentResult.description}");
            }

            var transaction = new PaymentTransaction
            {
                OrderCode = request.OrderCode,
                Amount = request.Amount,
                Description = request.Description,
                Status = "PENDING",
                UPId = upId,
                PaymentLinkId = createPaymentResult.paymentLinkId,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };
            _dbContext.PaymentTransactions.Add(transaction);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Payment link created: {createPaymentResult.checkoutUrl}");

            return new PaymentResponse
            {
                Code = createPaymentResult.status,
                Desc = createPaymentResult.description,
                Data = new PaymentResponseData
                {
                    PaymentLinkId = createPaymentResult.paymentLinkId,
                    CheckoutUrl = createPaymentResult.checkoutUrl
                }
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to create payment link for order: {request.OrderCode}");
            throw new Exception($"Failed to create payment link: {ex.Message}", ex);
        }
    }
    public async Task<PaymentLinkInformation> GetPaymentLinkInformation(long orderCode)
    {
        try
        {
            _logger.LogInformation($"Fetching payment link information for order: {orderCode}");
            var result = await _payOS.getPaymentLinkInformation(orderCode);
            _logger.LogInformation($"Payment link information fetched: {result.id}");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to fetch payment link information for order: {orderCode}");
            throw new Exception($"Failed to fetch payment link information: {ex.Message}");
        }
    }

    public async Task<PaymentLinkInformation> CancelPaymentLink(long orderCode, string cancellationReason = null)
    {
        try
        {
            _logger.LogInformation($"Canceling payment link for order: {orderCode}");
            var result = await _payOS.cancelPaymentLink(orderCode, cancellationReason);
            _logger.LogInformation($"Payment link canceled: {result.id}");
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Failed to cancel payment link for order: {orderCode}");
            throw new Exception($"Failed to cancel payment link: {ex.Message}");
        }
    }

    public WebhookData VerifyWebhookData(WebhookType webhookBody)
    {
        try
        {
            _logger.LogInformation("Verifying webhook data");
            var webhookData = _payOS.verifyPaymentWebhookData(webhookBody);
            _logger.LogInformation($"Webhook data verified for order: {webhookData.orderCode}");
            return webhookData;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to verify webhook data");
            throw new Exception($"Failed to verify webhook data: {ex.Message}");
        }
    }
}

public class PayOSCredentials
{
    public string ClientId { get; set; }
    public string ApiKey { get; set; }
    public string ChecksumKey { get; set; }
}