namespace EXE202_BE.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using EXE202_BE.Data.Models;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

[Route("api/identity")]
[ApiController]
public class IdentityController : ControllerBase
{
    private readonly UserManager<ModifyIdentityUser> _userManager;
    private readonly IEmailSender<ModifyIdentityUser> _emailSender;
    private readonly ILogger<IdentityController> _logger;

    public IdentityController(
        UserManager<ModifyIdentityUser> userManager,
        IEmailSender<ModifyIdentityUser> emailSender,
        ILogger<IdentityController> logger)
    {
        _userManager = userManager;
        _emailSender = emailSender;
        _logger = logger;
    }

    [HttpPost("forgotPassword")]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordDto model)
    {
        _logger.LogInformation($"Received forgot password request for email: {model.Email}");

        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            _logger.LogWarning($"User with email {model.Email} not found.");
            return Ok(new { message = "If the email exists, a password reset code has been sent." });
        }
        _logger.LogInformation($"User found: {user.UserName}, EmailConfirmed: {user.EmailConfirmed}");

        // Tạo mã code (dùng token làm mã code)
        var resetCode = await _userManager.GeneratePasswordResetTokenAsync(user);
        _logger.LogInformation($"Password reset code generated for user {user.UserName}: {resetCode}");

        try
        {
            await _emailSender.SendPasswordResetCodeAsync(user, user.Email, resetCode);
            _logger.LogInformation($"Password reset code email sent to {user.Email}.");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to send password reset code email to {user.Email}: {ex.Message}");
            return StatusCode(500, new { message = "Failed to send email. Please try again later." });
        }

        return Ok(new { message = "If the email exists, a password reset code has been sent." });
    }
    
    [HttpPost("resetPassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordDto model)
    {
        _logger.LogInformation($"Received reset password request for email: {model.Email}");

        var user = await _userManager.FindByEmailAsync(model.Email); // Tìm người dùng bằng email
        if (user == null)
        {
            _logger.LogWarning($"User with email {model.Email} not found.");
            return BadRequest(new { message = "Invalid user." });
        }

        var result = await _userManager.ResetPasswordAsync(user, model.ResetCode, model.NewPassword);
        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            _logger.LogError($"Failed to reset password for user {user.UserName}: {errors}");
            return BadRequest(new { message = errors });
        }

        _logger.LogInformation($"Password reset successfully for user {user.UserName}.");
        return Ok(new { message = "Password reset successfully." });
    }
}

public class ForgotPasswordDto
{
    public string Email { get; set; }
}

public class ResetPasswordDto
{
    public string Email { get; set; }
    public string ResetCode { get; set; } // Mã code từ email
    public string NewPassword { get; set; }
}

