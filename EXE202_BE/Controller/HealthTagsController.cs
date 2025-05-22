using EXE202_BE.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EXE202_BE.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HealthTagsController : ControllerBase
{
    private readonly IHealthTagsService _healthTagsService;

    public HealthTagsController(IHealthTagsService healthTagsService)
    {
        _healthTagsService = healthTagsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetHealthTags(
        [FromQuery] string? searchTerm,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        try
        {
            var healthTags = await _healthTagsService.GetHealthTagsAsync(searchTerm, page, pageSize);
            return Ok(healthTags);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while retrieving health tags.", Error = ex.Message });
        }
    }
}