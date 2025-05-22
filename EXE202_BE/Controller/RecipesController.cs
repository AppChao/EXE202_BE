using EXE202_BE.Data.DTOS.Recipe;
using EXE202_BE.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EXE202_BE.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RecipesController : ControllerBase
{
    private readonly IRecipesService _recipesService;

    public RecipesController(IRecipesService recipesService)
    {
        _recipesService = recipesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetRecipes(
        [FromQuery] string? searchTerm,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        try
        {
            var recipes = await _recipesService.GetRecipesAsync(searchTerm, page, pageSize);
            return Ok(recipes);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while retrieving recipes.", Error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRecipeById(int id)
    {
        try
        {
            var recipe = await _recipesService.GetRecipeByIdAsync(id);
            return Ok(recipe);
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while retrieving recipe.", Error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateRecipe([FromBody] RecipeRequest request)
    {
        try
        {
            var createdRecipe = await _recipesService.CreateRecipeAsync(request);
            return Ok(createdRecipe);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while creating recipe.", Error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRecipe(int id, [FromBody] RecipeRequest request)
    {
        try
        {
            var updatedRecipe = await _recipesService.UpdateRecipeAsync(id, request);
            return Ok(updatedRecipe);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while updating recipe.", Error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRecipe(int id)
    {
        try
        {
            await _recipesService.DeleteRecipeAsync(id);
            return NoContent();
        }
        catch (Exception ex)
        {
            return StatusCode(500, new { Message = "An error occurred while deleting recipe.", Error = ex.Message });
        }
    }
}