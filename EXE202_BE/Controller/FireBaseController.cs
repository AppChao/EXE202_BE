using EXE202_BE.Data.DTOS.Firebase;
using EXE202_BE.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EXE202_BE.Controllers;

[Route("api/[controller]")]
[ApiController]
public class FireBaseController : ControllerBase
{
    private readonly IFireBaseStorageService _fireBaseStorageService;

    public FireBaseController(IFireBaseStorageService fireBaseStorageService)
    {
        _fireBaseStorageService = fireBaseStorageService;
    }
    
    [HttpPost("recipe")]
    public async Task<IActionResult> UploadImage([FromForm] RecipeImage recipeImage)
    {
        if (recipeImage.recipeImage == null || recipeImage.recipeImage.Length == 0)
            return BadRequest("File is empty.");

        var fileName = Guid.NewGuid() + Path.GetExtension(recipeImage.recipeImage.FileName);
        var url = await _fireBaseStorageService.UploadImageAsync(recipeImage.recipeImage, fileName);

        return Ok(new { Url = url });
    }
    
    [HttpGet("fix")]
    public async Task<IActionResult> 
        Fix()
    {
        await _fireBaseStorageService.FixAllAppChaoImagesAsync();
        return Ok();
    }
}