using EXE202_BE.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EXE202_BE.Controllers;

[Route("api/[controller]")]
[ApiController]
public class IngredientsController : ControllerBase
{
    private readonly IIngredientsService _ingredientsService;

    public IngredientsController(IIngredientsService ingredientsService)
    {
        _ingredientsService = ingredientsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetIngredients(
        [FromQuery] string? searchTerm,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        try
        {
            var ingredients = await _ingredientsService.GetIngredientsAsync(searchTerm, page, pageSize);
            return Ok(ingredients);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while retrieving ingredients.", Error = ex.Message });
        }
    }
}