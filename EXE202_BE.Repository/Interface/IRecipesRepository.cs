using EXE202_BE.Data.Models;

namespace EXE202_BE.Repository.Interface;

public interface IRecipesRepository : IGenericRepository<Recipes>
{
    // Add custom methods here
    Task<List<Recipes>> GetRecipesByCategoryAsync(string? category);
    Task<List<Recipes>> GetRanDom();
}