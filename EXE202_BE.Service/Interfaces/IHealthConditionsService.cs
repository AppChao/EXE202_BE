using EXE202_BE.Data.DTOS;
using EXE202_BE.Data.Models;

namespace EXE202_BE.Service.Interface;

public interface IHealthConditionsService
{
    // Add custom methods here
    Task<PageListResponse<string>> GetHealthConditionTypesAsync(int page = 1, int pageSize = 20);
    Task<PageListResponse<HealthConditionResponse>> GetHealthConditionsByTypeAsync(string type, int page = 1, int pageSize = 20);
}