using EXE202_BE.Data.DTOS.User;
using EXE202_BE.Data.Models;

namespace EXE202_BE.Repository.Interface;

public interface IUserProfilesRepository : IGenericRepository<UserProfiles>
{
    // Add custom methods here
    Task UpdateAllergiesAsync(int upId, List<string> newAllergies);
    Task UpdatePersonalHealthConditionsAsync(int upId, List<HealthConditionDTO> newConditions);
}