using AutoMapper;
using EXE202_BE.Data.DTOS;
using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;
using EXE202_BE.Service.Interface;

namespace EXE202_BE.Service.Services;

public class HealthConditionsService : IHealthConditionsService
{
    private readonly IHealthConditionsRepository _healthConditionRepository;
    private readonly IMapper _mapper;

    public HealthConditionsService(
        IHealthConditionsRepository healthConditionRepository,
        IMapper mapper)
    {
        _healthConditionRepository = healthConditionRepository;
        _mapper = mapper;
    }

    public async Task<PageListResponse<string>> GetHealthConditionTypesAsync(string? searchTerm, int page = 1, int pageSize = 20)
    {
        return await _healthConditionRepository.GetHealthConditionTypesAsync(searchTerm, page, pageSize);
    }

    public async Task<PageListResponse<HealthConditionResponse>> GetHealthConditionsByTypeAsync(string? type, string? searchTerm, int page = 1, int pageSize = 20)
    {
        var response = await _healthConditionRepository.GetHealthConditionsAsync(type, searchTerm, page, pageSize);
        var result = _mapper.Map<List<HealthConditionResponse>>(response.Items);

        return new PageListResponse<HealthConditionResponse>
        {
            Items = result,
            Page = response.Page,
            PageSize = response.PageSize,
            TotalCount = response.TotalCount,
            HasNextPage = response.HasNextPage,
            HasPreviousPage = response.HasPreviousPage
        };
    }
}