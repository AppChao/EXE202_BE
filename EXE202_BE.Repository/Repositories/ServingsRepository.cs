using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;

namespace EXE202_BE.Repository.Repositories;

public class ServingsRepository : GenericRepository<Servings>, IServingsRepository
{
    public ServingsRepository(AppDbContext context) : base(context)
    {
    }
}