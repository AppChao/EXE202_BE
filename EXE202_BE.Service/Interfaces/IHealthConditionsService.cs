using EXE202_BE.Data.DTOS;
using EXE202_BE.Data.DTOS.Auth;
using EXE202_BE.Data.Models;

namespace EXE202_BE.Service.Interface;

public interface IHealthConditionsService
{
    // Add custom methods here
    Task<PageListResponse<string>> GetHealthConditionTypesAsync(string? searchTerm, int page = 1, int pageSize = 20);
    Task<PageListResponse<HealthConditionResponse>> GetHealthConditionsByTypeAsync(string? type, string? searchTerm,
        int page = 1, int pageSize = 20);
    Task<List<PersonalHealthConditions>> CreateHealthConditions(UserProfiles info, SignUpRequest model);
}