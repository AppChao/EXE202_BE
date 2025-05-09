using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;

namespace EXE202_BE.Repository.Repositories;

public class AllergiesRepository : GenericRepository<Allergies>, IAllergiesRepository
{
    public AllergiesRepository(AppDbContext context) : base(context)
    {
    }
}