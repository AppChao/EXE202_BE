using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;

namespace EXE202_BE.Repository.Repositories;

public class HealthTagsRepository : GenericRepository<HealthTags>, IHealthTagsRepository
{
    public HealthTagsRepository(AppDbContext context) : base(context)
    {
    }
}