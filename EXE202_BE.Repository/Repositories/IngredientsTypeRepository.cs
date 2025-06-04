using EXE202_BE.Data.DTOS;
using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EXE202_BE.Repository.Repositories;

public class IngredientsTypeRepository : GenericRepository<IngredientTypes>, IIngredientsTypeRepository
{
    public IngredientsTypeRepository(AppDbContext context) : base(context)
    {
    }
    public async Task<PageListResponse<IngredientTypes>> GetIngredientTypesAsync(string? searchTerm, int page = 1, int pageSize = 20)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 20;

        IQueryable<IngredientTypes> query = DbSet;

        if (!string.IsNullOrEmpty(searchTerm))
        {
            searchTerm = searchTerm.ToLower();
            query = query.Where(t => t.TypeName != null && t.TypeName.ToLower().Contains(searchTerm));
        }

        var totalCount = await query.CountAsync();

        var paginatedItems = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PageListResponse<IngredientTypes>
        {
            Items = paginatedItems,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            HasNextPage = (page * pageSize) < totalCount,
            HasPreviousPage = page > 1
        };
    }
}
