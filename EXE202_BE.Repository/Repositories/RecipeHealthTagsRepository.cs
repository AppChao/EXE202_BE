using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;

namespace EXE202_BE.Repository.Repositories;

public class RecipeHealthTagsRepository : GenericRepository<RecipeHealthTags>, IRecipeHealthTagsRepository
{
    public RecipeHealthTagsRepository(AppDbContext context) : base(context)
    {
    }
}