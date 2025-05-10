using EXE202_BE.Data.DTOS.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EXE202_BE.Service.Interface;

namespace EXE202_BE.Controllers;

[Route("api/[controller]")]
[ApiController]
//[Authorize(Roles = "Admin")]
public class UserProfileController : ControllerBase
{
    private readonly IUserProfilesService _userProfilesService;

    public UserProfileController(IUserProfilesService userProfilesService)
    {
        _userProfilesService = userProfilesService;
    }

    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] CreateUserRequestDTO model)
    {
        try
        {
            var response = await _userProfilesService.CreateUserAsync(model);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Failed to create user.", Error = ex.Message });
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetUsers([FromQuery] string? searchTerm)
    {
        try
        {
            var users = await _userProfilesService.GetUsersAsync(searchTerm);
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while retrieving users.", Error = ex.Message });
        }
    }

    [HttpGet("{upId}")]
    public async Task<IActionResult> GetUserById(int upId)
    {
        try
        {
            var user = await _userProfilesService.GetUserByIdAsync(upId);
            return Ok(user);
        }
        catch (Exception ex)
        {
            return NotFound(new { Message = "User not found.", Error = ex.Message });
        }
    }

    [HttpPut("{upId}")]
    public async Task<IActionResult> Edit(int upId, [FromBody] EditUserRequestDTO model)
    {
        try
        {
            var response = await _userProfilesService.EditAsync(upId, model);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Failed to update user.", Error = ex.Message });
        }
    }
}