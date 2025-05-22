using EXE202_BE.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EXE202_BE.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MealCatagoriesController : ControllerBase
{
    private readonly IMealCatagoriesService _mealCatagoriesService;

    public MealCatagoriesController(IMealCatagoriesService mealCatagoriesService)
    {
        _mealCatagoriesService = mealCatagoriesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetMealCategories(
        [FromQuery] string? searchTerm,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        try
        {
            var mealCategories = await _mealCatagoriesService.GetMealCategoriesAsync(searchTerm, page, pageSize);
            return Ok(mealCategories);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while retrieving meal categories.", Error = ex.Message });
        }
    }
}