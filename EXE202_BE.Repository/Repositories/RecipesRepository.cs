using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EXE202_BE.Repository.Repositories;

public class RecipesRepository : GenericRepository<Recipes>, IRecipesRepository
{
    private readonly AppDbContext _context;
    public RecipesRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
    
    public async Task<List<Recipes>> GetRecipesByCategoryAsync(string? meal)
    {
        IQueryable<Recipes> query = DbSet;

        if (!string.IsNullOrEmpty(meal) && meal.ToLower() != "all")
        {
            query = query.Where(r => r.Meals != null && r.Meals.ToLower() == meal.ToLower());
        }

        return await query.ToListAsync();
    }

    public async Task<List<Recipes>> GetRanDom()
    {
        return await _context.Recipes.Take(3).ToListAsync();
    }
}