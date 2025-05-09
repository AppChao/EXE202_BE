using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;

namespace EXE202_BE.Repository.Repositories;

public class PersonalHealthConditionsRepository : GenericRepository<PersonalHealthConditions>, IPersonalHealthConditionsRepository
{
    public PersonalHealthConditionsRepository(AppDbContext context) : base(context)
    {
    }
}