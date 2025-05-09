using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;

namespace EXE202_BE.Repository.Repositories;

public class RecipesRepository : GenericRepository<Recipes>, IRecipesRepository
{
    public RecipesRepository(AppDbContext context) : base(context)
    {
    }
}