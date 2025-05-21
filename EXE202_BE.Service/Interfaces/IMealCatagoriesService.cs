using EXE202_BE.Data.DTOS;
using EXE202_BE.Data.DTOS.MealCategory;
using EXE202_BE.Data.Models;

namespace EXE202_BE.Service.Interface;

public interface IMealCatagoriesService
{
    // Add custom methods here
    Task<PageListResponse<MealCategoryResponse>> GetMealCategoriesAsync(string? searchTerm, int page, int pageSize);
}