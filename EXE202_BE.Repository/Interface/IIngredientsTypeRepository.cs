using EXE202_BE.Data.DTOS;
using EXE202_BE.Data.Models;

namespace EXE202_BE.Repository.Interface;

public interface IIngredientsTypeRepository : IGenericRepository<IngredientTypes>
{
    Task<PageListResponse<IngredientTypes>> GetIngredientTypesAsync(string? searchTerm, int page = 1, int pageSize = 20);
    
}