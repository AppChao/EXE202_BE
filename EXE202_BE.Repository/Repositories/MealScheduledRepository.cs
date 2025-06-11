using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;

namespace EXE202_BE.Repository.Repositories;

public class MealScheduledRepository : GenericRepository<MealScheduled>, IMealScheduledRepository
{
    private readonly AppDbContext _dbContext;
    
    public MealScheduledRepository(AppDbContext db) : base(db)
    {
        _dbContext = db;
    }
}