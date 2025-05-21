using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;

namespace EXE202_BE.Repository.Repositories;

public class IngredientsRepository : GenericRepository<Ingredients>, IIngredientsRepository
{
public IngredientsRepository(AppDbContext context) : base(context)
{
}
}