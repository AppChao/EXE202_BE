using EXE202_BE.Data.DTOS;
using EXE202_BE.Data.DTOS.Ingredient;

namespace EXE202_BE.Service.Interface;

public interface IIngredientsService
{
    Task<PageListResponse<Ingredient1Response>> GetIngredientsAsync(string? searchTerm, int page, int pageSize);
    
    Task<PageListResponse<IngredientTypeResponse>> GetIngredientTypesAsync(string? searchTerm, int page = 1, int pageSize = 20);
    Task<PageListResponse<IngredientResponse>> GetIngredientsByTypeAsync(int? typeId, string? searchTerm, int page = 1, int pageSize = 20);
    
    Task<List<CommonAllergenResponse>?>? GetCommonAllergensAsync();
}