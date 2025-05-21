using EXE202_BE.Data.DTOS;
using EXE202_BE.Data.DTOS.Ingredient;

namespace EXE202_BE.Service.Interface;

public interface IIngredientsService
{
    Task<PageListResponse<IngredientResponse>> GetIngredientsAsync(string? searchTerm, int page, int pageSize);
}