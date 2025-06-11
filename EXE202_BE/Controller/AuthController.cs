using EXE202_BE.Data.DTOS.Auth;
using EXE202_BE.Data.DTOS.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EXE202_BE.Service.Interface;
using Google.Apis.Auth;

namespace EXE202_BE.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<UserProfileController> _logger;

    public AuthController(
        IAuthService authService,
        ILogger<UserProfileController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequestDTO model)
    {
        try
        {
            var response = await _authService.LoginAsync(model);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return Unauthorized(new { Message = "Login failed.", Error = ex.Message });
        }
    }

    [HttpPost("customer/login")]
    public async Task<IActionResult> CustomerLogin(LoginRequestDTO model)
    {
        try
        {
            var response = await _authService.CustomerLoginAsync(model);
            return Ok(response);
            
        } 
        catch (Exception ex)
        {
            return Unauthorized(new { Message = "Login failed.", Error = ex.Message });
        }
    }
    
    [HttpPut("change-password")]
    [Authorize]
    public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequest request)
    {
        try
        {
            await _authService.ChangePasswordAsync(request);
            return Ok(new { Message = "Password changed successfully" });
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning(ex, "Unauthorized access attempt in ChangePassword.");
            return Unauthorized(new { message = ex.Message });
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning(ex, "User not found in ChangePassword.");
            return NotFound(new { message = ex.Message });
        }
        catch (ArgumentException ex)
        {
            _logger.LogInformation(ex, "Invalid input in ChangePassword.");
            return BadRequest(new { message = ex.Message });
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogInformation(ex, "Password change failed.");
            return BadRequest(new { message = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error in ChangePassword.");
            return StatusCode(500, new { message = "An unexpected error occurred." });
        }
    }

    [HttpPost("google")]
    public async Task<IActionResult> GoogleLogin([FromBody] LoginGoogleRequest model)
    {
        try
        {
            var response = await _authService.LoginGoogleAsync(model);
            return Ok(response);
            
        } 
        catch (Exception ex)
        {
            return Unauthorized(new { Message = "Login failed.", Error = ex.Message });
        }    
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp([FromBody] SignUpRequest model)
    {
        try
        {
            var response = await _authService.SignUp(model);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return Unauthorized(new { Message = "Sign up failed.", Error = ex.Message });
        }
    }
}