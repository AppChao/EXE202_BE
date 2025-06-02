using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;

namespace EXE202_BE.Repository.Repositories;

public class IngredientsTypeRepository : GenericRepository<IngredientTypes>, IIngredientsTypeRepository
{
public IngredientsTypeRepository(AppDbContext context) : base(context)
{
}
}
