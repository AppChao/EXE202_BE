using EXE202_BE.Data.DTOS.Ingredient;
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
    
    [HttpGet("ingredient-types")]
    public async Task<IActionResult> GetIngredientTypes(
        [FromQuery] string? searchTerm,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        try
        {
            var types = await _ingredientsService.GetIngredientTypesAsync(searchTerm, page, pageSize);
            return Ok(types);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Lấy danh sách loại nguyên liệu thất bại.", Error = ex.Message });
        }
    }

    [HttpGet("ingredients")]
    public async Task<IActionResult> GetIngredientsByType(
        [FromQuery] int? typeId,
        [FromQuery] string? searchTerm,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        try
        {
            var ingredients = await _ingredientsService.GetIngredientsByTypeAsync(typeId, searchTerm, page, pageSize);
            return Ok(ingredients);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Lấy danh sách nguyên liệu thất bại.", Error = ex.Message });
        }
    }

    [HttpGet("common/allergens")]
    public async Task<IActionResult> GetCommonAllergens()
    {
        try
        {
            var allergens = await _ingredientsService.GetCommonAllergensAsync()!;
            if (allergens == null)
            {
                return BadRequest(new { Message = "Lấy danh sách nguyên liệu thất bại.", Error = "danh sach null" });
            }
            return Ok(allergens);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Lấy danh sách nguyên liệu thất bại.", Error = ex.Message });
        }
    }
}