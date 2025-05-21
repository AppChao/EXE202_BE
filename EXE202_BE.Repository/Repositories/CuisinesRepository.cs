using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;

namespace EXE202_BE.Repository.Repositories;

public class CuisinesRepository : GenericRepository<Cuisines>, ICuisinesRepository
{
public CuisinesRepository(AppDbContext context) : base(context)
{
}
}