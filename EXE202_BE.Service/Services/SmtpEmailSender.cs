using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.Net;
using System.Threading.Tasks;
using EXE202_BE.Data.Models;
using EXE202_BE.Data.DTOS.PayOS;
using EXE202_BE.Service.Interface;
using EXE202_BE.Service.Services;
using Microsoft.EntityFrameworkCore;
using Net.payOS.Types;

public class SmtpEmailSender : ICustomEmailSender<ModifyIdentityUser>
{
    private readonly IConfiguration _config;
    private readonly ILogger<SmtpEmailSender> _logger;
    private readonly PayOSService _payOSService;
    private readonly AppDbContext _dbContext;

    public SmtpEmailSender(
        IConfiguration config, 
        ILogger<SmtpEmailSender> logger,
        PayOSService payOSService,
        AppDbContext dbContext)
    {
        _config = config;
        _logger = logger;
        _payOSService = payOSService;
        _dbContext = dbContext;
    }

    public async Task SendConfirmationLinkAsync(ModifyIdentityUser user, string email, string confirmationLink)
    {
        // Giữ nguyên
        string subject = "Confirm Your Email";
        string body = $@"
        <div style='font-family: Arial, sans-serif; padding: 20px; background-color: #f4f4f4;'>
            <div style='max-width: 600px; background: white; padding: 20px; border-radius: 8px; box-shadow: 0px 0px 10px rgba(0,0,0,0.1);'>
                <h2 style='color: #333;'>Confirm Your Email</h2>
                <p style='font-size: 16px; color: #555;'>Hello {user.UserName},</p>
                <p style='font-size: 16px; color: #555;'>Please confirm your email by clicking the button below:</p>
                <a href='{confirmationLink}' style='display: inline-block; padding: 10px 20px; color: #fff; background: #28a745; text-decoration: none; border-radius: 5px; font-size: 16px;'>Confirm Email</a>
                <p style='font-size: 14px; color: #888;'>If you did not request this, please ignore this email.</p>
            </div>
        </div>";
        await SendEmailAsync(email, subject, body);
    }

    public async Task SendPasswordResetLinkAsync(ModifyIdentityUser user, string email, string resetLink)
    {
        // Giữ nguyên
        _logger.LogInformation($"Sending password reset link to {email} for user {user.UserName}. Reset link: {resetLink}");
        string subject = "Reset Your Password";
        string body = $@"
        <div style='font-family: Arial, sans-serif; padding: 20px; background-color: #f4f4f4;'>
            <div style='max-width: 600px; background: white; padding: 20px; border-radius: 8px; box-shadow: 0px 0px 10px rgba(0,0,0,0.1);'>
                <h2 style='color: #333;'>Reset Your Password</h2>
                <p style='font-size: 16px; color: #555;'>Hello {user.UserName},</p>
                <p style='font-size: 16px; color: #555;'>You can reset your password by clicking the button below:</p>
                <a href='{resetLink}' style='display: inline-block; padding: 10px 20px; color: #fff; background: #dc3545; text-decoration: none; border-radius: 5px; font-size: 16px;'>Reset Password</a>
                <p style='font-size: 14px; color: #888;'>If you did not request this, please ignore this email.</p>
            </div>
        </div>";
        await SendEmailAsync(email, subject, body);
    }

    public async Task SendPasswordResetCodeAsync(ModifyIdentityUser user, string email, string resetCode)
    {
        // Giữ nguyên
        _logger.LogInformation($"Sending password reset code to {email} for user {user.UserName}. Reset code: {resetCode}");

        string subject = "Your Password Reset Code";
        string body = $@"
        <div style='font-family: Arial, sans-serif; padding: 20px; background-color: #f4f4f4;'>
            <div style='max-width: 600px; background: white; padding: 20px; border-radius: 8px; box-shadow: 0px 0px 10px rgba(0,0,0,0.1);'>
                <h2 style='color: #333;'>Password Reset Code</h2>
                <p style='font-size: 16px; color: #555;'>Hello {user.UserName},</p>
                <p style='font-size: 16px; color: #555;'>Your password reset code is:</p>
                <div style='font-size: 20px; font-weight: bold; color: #007bff; background: #e9ecef; padding: 10px; display: inline-block; border-radius: 5px;'>{resetCode}</div>
                <p style='font-size: 14px; color: #888;'>Use this code to reset your password. It will expire soon.</p>
            </div>
        </div>";
        await SendEmailAsync(email, subject, body);
    }

    public async Task SendSubscriptionRenewReminderAsync(ModifyIdentityUser user, string email, DateTime? endDate)
    {
        _logger.LogInformation($"Sending subscription renew reminder to {email} for user {user.UserName}. End date: {endDate}");

        // Tìm UserProfile dựa vào email
        var userProfile = await _dbContext.UserProfiles
            .FirstOrDefaultAsync(up => up.UserId == user.Id);
        if (userProfile == null)
        {
            _logger.LogWarning($"UserProfile not found for user {user.Id} with email {email}");
            return;
        }

        // Xác định gói subscription hiện tại
        int amount;
        string planName;
        switch (userProfile.SubcriptionId)
        {
            case 2: // vip1
                amount = 10000;
                planName = "vip1";
                break;
            case 3: // vip2
                amount = 15000;
                planName = "vip2";
                break;
            default:
                _logger.LogWarning($"User {user.Id} has invalid subscriptionId: {userProfile.SubcriptionId}. Skipping renewal reminder.");
                return;
        }

        // Tạo payment link
        var request = new PaymentRequest
        {
            OrderCode = 0, // Sẽ được backend tạo
            Amount = amount,
            Description = $"Renew for UPId: {userProfile.UPId}",
            BuyerName = user.UserName,
            BuyerEmail = user.Email,
            BuyerPhone = user.PhoneNumber ?? "0123456789",
            Items = new List<ItemData>
            {
                new ItemData(planName, 1, amount)
            }
        };

        var upId = userProfile.UPId;
        var response = await _payOSService.CreatePaymentLink(request, upId);
        var checkoutUrl = response.Data.CheckoutUrl;

        // Gửi email với link dẫn thẳng đến checkoutUrl
        string subject = "Renew Your Subscription";
        string body = $@"
        <div style='font-family: Arial, sans-serif; padding: 20px; background-color: #f4f4f4;'>
            <div style='max-width: 600px; background: white; padding: 20px; border-radius: 8px; box-shadow: 0px 0px 10px rgba(0,0,0,0.1);'>
                <h2 style='color: #333;'>Renew Your Subscription</h2>
                <p style='font-size: 16px; color: #555;'>Hello {user.UserName},</p>
                <p style='font-size: 16px; color: #555;'>Your subscription will expire on {endDate?.ToString("yyyy-MM-dd")}.</p>
                <p style='font-size: 16px; color: #555;'>Please renew your subscription to continue enjoying premium features:</p>
                <a href='{checkoutUrl}' style='display: inline-block; padding: 10px 20px; color: #fff; background: #007bff; text-decoration: none; border-radius: 5px; font-size: 16px;'>Renew Now</a>
                <p style='font-size: 14px; color: #888;'>If you do not wish to renew, your account will revert to the free plan after expiration.</p>
            </div>
        </div>";
        await SendEmailAsync(email, subject, body);
    }

    private async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var smtpHost = _config["EmailSettings:SmtpHost"];
        var smtpPort = int.Parse(_config["EmailSettings:SmtpPort"]);
        var smtpUser = _config["EmailSettings:SmtpUser"];
        var smtpPass = _config["EmailSettings:SmtpPass"];
        var fromEmail = _config["EmailSettings:FromEmail"];
        var fromName = _config["EmailSettings:FromName"];
        
        _logger.LogInformation($"SMTP Config - Host: {smtpHost}, Port: {smtpPort}, User: {smtpUser}, FromEmail: {fromEmail}, FromName: {fromName}");

        try
        {
            var client = new SmtpClient(smtpHost)
            {
                Port = smtpPort,
                Credentials = new NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true
            };

            var mailMessage = new MailMessage
            {
                From = new MailAddress(fromEmail, fromName),
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            };

            mailMessage.To.Add(toEmail);
            await client.SendMailAsync(mailMessage);
            _logger.LogInformation($"Email sent to {toEmail}");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to send email: {ex.Message}");
            throw;
        }
    }
}