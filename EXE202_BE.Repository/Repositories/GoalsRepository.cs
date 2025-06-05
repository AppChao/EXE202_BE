using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;

namespace EXE202_BE.Repository.Repositories;

public class GoalsRepository : GenericRepository<Goals>, IGoalsRepository
{
    public GoalsRepository(AppDbContext context) : base(context)
    {
    }
}