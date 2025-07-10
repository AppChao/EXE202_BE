using EXE202_BE.Data.DTOS;
using EXE202_BE.Data.DTOS.Recipe;
using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;

namespace EXE202_BE.Service.Interface;

public interface IRecipesService
{
    Task<PageListResponse<RecipeResponse>> GetRecipesAsync(string? searchTerm, int page, int pageSize);
    Task<RecipeResponse> GetRecipeByIdAsync(int id);
    Task<RecipeResponse> CreateRecipeAsync(RecipeRequest request);
    Task<RecipeResponse> UpdateRecipeAsync(int id, RecipeRequest request);
    Task DeleteRecipeAsync(int id);
    Task<PageListResponse<RecipeHomeResponse>> GetRecipesHomeAsync(string? category, string? searchTerm, int page = 1, int pageSize = 14);
    Task<List<RecipeResponse>> GetRandom();
}