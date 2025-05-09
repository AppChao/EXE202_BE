using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;

namespace EXE202_BE.Repository.Repositories;

public class HealthConditionsRepository : GenericRepository<HealthConditions>, IHealthConditionsRepository
{
    public HealthConditionsRepository(AppDbContext context) : base(context)
    {
    }
}