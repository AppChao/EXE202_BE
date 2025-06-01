using EXE202_BE.Service.Services;
using Net.payOS.Types;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using EXE202_BE.Data.Models;
using EXE202_BE.Data.DTOS.PayOS;
using EXE202_BE.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace EXE202_BE.Controllers;

[Route("api/payment")]
[ApiController]
public class PaymentController : ControllerBase
{
    private readonly PayOSService _payOSService;
    private readonly AppDbContext _dbContext;
    private readonly SubscriptionExpirationJob _subscriptionExpirationJob;
    private readonly UserManager<ModifyIdentityUser> _userManager;
    private readonly ILogger<PaymentController> _logger;

    public PaymentController(
        PayOSService payOSService,
        AppDbContext dbContext,
        UserManager<ModifyIdentityUser> userManager,
        ILogger<PaymentController> logger,
        SubscriptionExpirationJob subscriptionExpirationJob)
    {
        _payOSService = payOSService;
        _dbContext = dbContext;
        _userManager = userManager;
        _logger = logger;
        _subscriptionExpirationJob = subscriptionExpirationJob;
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreatePayment([FromBody] PaymentRequest request, [FromQuery] int upId)
    {
        _logger.LogInformation($"Received payment request for order: {request?.OrderCode}, amount: {request?.Amount}, upId: {upId}");

        if (upId <= 0)
        {
            return BadRequest(new { message = "Yêu cầu UPId hợp lệ." });
        }

        if (request == null || request.Amount <= 0 || string.IsNullOrEmpty(request.Description))
        {
            return BadRequest(new { message = "Dữ liệu yêu cầu thanh toán không hợp lệ." });
        }

        var userProfile = await _dbContext.UserProfiles.FindAsync(upId);
        if (userProfile == null)
        {
            _logger.LogWarning($"Không tìm thấy UserProfile với UPId {upId}.");
            return BadRequest(new { message = "UserProfile không hợp lệ." });
        }

        try
        {
            var response = await _payOSService.CreatePaymentLink(request, upId);
            if (response.Code != "PENDING")
            {
                _logger.LogError($"Tạo link thanh toán thất bại: {response.Desc}");
                return BadRequest(new { message = response.Desc });
            }

            _logger.LogInformation($"Đã tạo link thanh toán: {response.Data.CheckoutUrl}");
            return Ok(new { checkoutUrl = response.Data.CheckoutUrl });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Tạo link thanh toán thất bại: {ex.Message}");
            return StatusCode(500, new { message = "Tạo link thanh toán thất bại. Vui lòng thử lại sau." });
        }
    }

    [HttpPost("webhook")]
    public async Task<IActionResult> Webhook([FromBody] WebhookType webhookBody)
    {
        _logger.LogDebug($"Webhook payload: {JsonSerializer.Serialize(webhookBody)}");

        try
        {
            var webhookData = _payOSService.VerifyWebhookData(webhookBody);
            _logger.LogInformation($"Nhận webhook từ PayOS: orderCode={webhookData.orderCode}, code={webhookData.code}, desc={webhookData.desc}");

            // Bỏ qua nếu là test webhook với orderCode cố định 123
            if (webhookData.orderCode == 123)
            {
                _logger.LogInformation("Bỏ qua webhook test với orderCode 123.");
                return Ok(new { message = "Webhook test được bỏ qua." });
            }

            var transaction = await _dbContext.PaymentTransactions
                .FirstOrDefaultAsync(t => t.OrderCode == webhookData.orderCode);
            if (transaction == null)
            {
                _logger.LogWarning($"Không tìm thấy giao dịch với orderCode {webhookData.orderCode}.");
                return BadRequest(new { message = "Không tìm thấy giao dịch." });
            }

            string transactionStatus = webhookData.code switch
            {
                "00" => "PAID",
                _ => "FAILED"
            };

            if (transaction.Status == transactionStatus)
            {
                _logger.LogInformation($"Giao dịch {webhookData.orderCode} đã được xử lý với trạng thái {transactionStatus}.");
                return Ok(new { message = "Webhook đã được xử lý." });
            }

            transaction.Status = transactionStatus;
            transaction.UpdatedAt = DateTime.UtcNow;
            await _dbContext.SaveChangesAsync();

            if (transactionStatus == "PAID")
            {
                var userProfile = await _dbContext.UserProfiles
                    .FirstOrDefaultAsync(up => up.UPId == transaction.UPId);
                if (userProfile != null)
                {
                    // Cập nhật subscriptionId dựa trên amount
                    userProfile.SubcriptionId = transaction.Amount switch
                    {
                        10000 => 2, // vip1
                        15000 => 3, // vip2
                        _ => userProfile.SubcriptionId // Giữ nguyên nếu không khớp
                    };
                    
                    // Cập nhật thời hạn subscription (30 ngày)
                    userProfile.StartDate = DateTime.UtcNow;
                    userProfile.EndDate = DateTime.UtcNow.AddDays(30);
                    
                    await _dbContext.SaveChangesAsync();
                    _logger.LogInformation($"Đã cập nhật SubcriptionId thành {userProfile.SubcriptionId} cho UPId {transaction.UPId}.");
                }
                else
                {
                    _logger.LogWarning($"Không tìm thấy UserProfile cho UPId {transaction.UPId}.");
                }
            }

            return Ok(new { message = "Webhook được xử lý thành công." });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Lỗi xử lý webhook: {ex.Message}, StackTrace: {ex.StackTrace}");
            return StatusCode(500, new { message = "Lỗi khi xử lý webhook." });
        }
    }

    [HttpGet("info/{orderCode}")]
    public async Task<IActionResult> GetPaymentInfo(long orderCode)
    {
        try
        {
            var paymentInfo = await _payOSService.GetPaymentLinkInformation(orderCode);
            return Ok(paymentInfo);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Lỗi khi lấy thông tin thanh toán cho order: {orderCode}");
            return StatusCode(500, new { message = "Lỗi khi lấy thông tin thanh toán." });
        }
    }

    [HttpPost("cancel/{orderCode}")]
    public async Task<IActionResult> CancelPayment(long orderCode, [FromBody] string cancellationReason = null)
    {
        try
        {
            var result = await _payOSService.CancelPaymentLink(orderCode, cancellationReason);
            return Ok(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Lỗi khi hủy link thanh toán cho order: {orderCode}");
            return StatusCode(500, new { message = "Lỗi khi hủy link thanh toán." });
        }
    }
    
    [HttpPost("test-notify-expiring")]
    public async Task<IActionResult> TestNotifyExpiringSubscriptions()
    {
        await _subscriptionExpirationJob.NotifyExpiringSubscriptions();
        return Ok("Triggered NotifyExpiringSubscriptions job.");
    }
    
    [HttpPost("test-check-expired")]
    public async Task<IActionResult> TestCheckExpiredSubscriptions()
    {
        await _subscriptionExpirationJob.CheckAndUpdateExpiredSubscriptions();
        return Ok("Triggered CheckAndUpdateExpiredSubscriptions job.");
    }
}