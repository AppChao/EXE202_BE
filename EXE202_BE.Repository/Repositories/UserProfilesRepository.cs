using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;

namespace EXE202_BE.Repository.Repositories;

public class UserProfilesRepository : GenericRepository<UserProfiles>, IUserProfilesRepository
{
    public UserProfilesRepository(AppDbContext context) : base(context)
    {
    }
}