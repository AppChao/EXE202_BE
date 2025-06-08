using EXE202_BE.Data.DTOS;
using EXE202_BE.Data.Models;

namespace EXE202_BE.Repository.Interface;

public interface IIngredientsRepository : IGenericRepository<Ingredients>
{
    Task<PageListResponse<Ingredients>> GetIngredientsAsync(int? typeId, string? searchTerm, int page = 1, int pageSize = 20);
    
    Task<List<Ingredients>?> GetAllIngredientsOrderByIconAsync();
}