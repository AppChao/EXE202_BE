using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;

namespace EXE202_BE.Repository.Repositories;

public class SubcriptionsRepository : GenericRepository<Subcriptions>, ISubcriptionsRepository
{
    public SubcriptionsRepository(AppDbContext context) : base(context)
    {
    }
}