using EXE202_BE.Data.DTOS.User;
using EXE202_BE.Data.Models;

namespace EXE202_BE.Service.Interface;

public interface IUserProfilesService
{
    Task<UserProfiles> AddAsync(UserProfiles userProfile);
    
    Task<UserProfileResponse> CreateUserAsync(CreateUserRequestDTO model);
    Task<List<UserProfileResponse>> GetUsersAsync(string? searchTerm);
    Task<UserProfileResponse> GetUserByIdAsync(int upId);
    Task<UserProfileResponse> EditAsync(int upId, EditUserRequestDTO model);
}