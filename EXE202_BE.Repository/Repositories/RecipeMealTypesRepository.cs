using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;

namespace EXE202_BE.Repository.Repositories;

public class RecipeMealTypesRepository : GenericRepository<RecipeMealTypes>, IRecipeMealTypesRepository
{
    public RecipeMealTypesRepository(AppDbContext context) : base(context)
    {
    }
}