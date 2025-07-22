using Microsoft.AspNetCore.Http;

namespace EXE202_BE.Data.DTOS.Firebase;

public class RecipeImage
{
    public IFormFile recipeImage { get; set; }
}