using EXE202_BE.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EXE202_BE.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CuisinesController : ControllerBase
{
    private readonly ICuisinesService _cuisinesService;

    public CuisinesController(ICuisinesService cuisinesService)
    {
        _cuisinesService = cuisinesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetCuisines(
        [FromQuery] string? searchTerm,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        try
        {
            var cuisines = await _cuisinesService.GetCuisinesAsync(searchTerm, page, pageSize);
            return Ok(cuisines);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while retrieving cuisines.", Error = ex.Message });
        }
    }
}