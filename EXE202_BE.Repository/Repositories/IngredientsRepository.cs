using EXE202_BE.Data.DTOS;
using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EXE202_BE.Repository.Repositories;

public class IngredientsRepository : GenericRepository<Ingredients>, IIngredientsRepository
{
    public IngredientsRepository(AppDbContext context) : base(context)
    {
    }
    
    public async Task<PageListResponse<Ingredients>> GetIngredientsAsync(int? typeId, string? searchTerm, int page = 1, int pageSize = 20)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 20;

        IQueryable<Ingredients> query = DbSet;

        if (typeId.HasValue)
        {
            query = query.Where(i => i.IngredientTypeId == typeId.Value);
        }

        if (!string.IsNullOrEmpty(searchTerm))
        {
            searchTerm = searchTerm.ToLower();
            query = query.Where(i => i.IngredientName != null && i.IngredientName.ToLower().Contains(searchTerm));
        }

        var totalCount = await query.CountAsync();

        var paginatedItems = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new PageListResponse<Ingredients>
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