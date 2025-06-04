using EXE202_BE.Data.DTOS;
using EXE202_BE.Data.Models;

namespace EXE202_BE.Repository.Interface;

public interface IHealthConditionsRepository : IGenericRepository<HealthConditions>
{
    // Add custom methods here
    Task<PageListResponse<HealthConditions>> GetHealthConditionsAsync(string? type, string? searchTerm, int page = 1, int pageSize = 20);
    Task<PageListResponse<string>> GetHealthConditionTypesAsync(string? searchTerm, int page = 1, int pageSize = 20);
}