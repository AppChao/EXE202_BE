using EXE202_BE.Data.DTOS.Auth;
using EXE202_BE.Data.DTOS.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EXE202_BE.Service.Interface;

namespace EXE202_BE.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
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

    
}