using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EXE202_BE.Repository.Repositories;

public class RecipesRepository : GenericRepository<Recipes>, IRecipesRepository
{
    public RecipesRepository(AppDbContext context) : base(context)
    {
    }
    
    public async Task<List<Recipes>> GetRecipesByCategoryAsync(string? category)
    {
        IQueryable<Recipes> query = DbSet
            .Include(r => r.RecipeMealTypes)
            .ThenInclude(rmt => rmt.MealCatagorie);

        if (!string.IsNullOrEmpty(category) && category.ToLower() != "all")
        {
            query = query.Where(r => r.RecipeMealTypes.Any(rmt => rmt.MealCatagorie.MealName.ToLower() == category.ToLower()));
        }

        return await query.ToListAsync();
    }
}