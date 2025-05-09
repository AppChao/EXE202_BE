using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;

namespace EXE202_BE.Repository.Repositories;

public class MealCatagoriesRepository : GenericRepository<MealCatagories>, IMealCatagoriesRepository
{
    public MealCatagoriesRepository(AppDbContext context) : base(context)
    {
    }
}