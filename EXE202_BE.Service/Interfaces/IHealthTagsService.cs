using EXE202_BE.Data.DTOS;
using EXE202_BE.Data.DTOS.HealthTag;
using EXE202_BE.Data.Models;

namespace EXE202_BE.Service.Interface;

public interface IHealthTagsService
{
    Task<PageListResponse<HealthTagResponse>> GetHealthTagsAsync(string? searchTerm, int page, int pageSize);
}