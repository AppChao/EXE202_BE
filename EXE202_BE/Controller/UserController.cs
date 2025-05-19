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
    public async Task<IActionResult> GetUsers(
        [FromQuery] string? searchTerm,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        try
        {
            var users = await _userProfilesService.GetUsersAsync(searchTerm, page, pageSize);
            return Ok(users);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while retrieving users.", Error = ex.Message });
        }
    }


    [HttpGet("userProfile/{upId}")]
    public async Task<IActionResult> GetUserProfile(int upId)
    {
        try
        {
            var userProfile = await _userProfilesService.GetUserProfileAsync(upId);
            return Ok(userProfile);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while retrieving user profile.", Error = ex.Message });
        }
    }

    [HttpGet("userProfileMin/{upId}")]
    public async Task<IActionResult> GetAdminProfile(int upId)
    {
        try
        {
            var adminProfile = await _userProfilesService.GetAdminProfileAsync(upId);
            return Ok(adminProfile);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while retrieving admin profile.", Error = ex.Message });
        }
    }

    [HttpPut("userProfile/{upId}")]
    public async Task<IActionResult> UpdateAdminProfile(int upId, [FromBody] AdminProfileResponse request)
    {
        try
        {
            if (upId != request.UPId)
            {
                return BadRequest(new { Message = "User ID mismatch." });
            }
            var updatedProfile = await _userProfilesService.UpdateAdminProfileAsync(upId, request);
            return Ok(updatedProfile);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while updating admin profile.", Error = ex.Message });
        }
    }
}