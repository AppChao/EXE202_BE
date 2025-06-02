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

    public async Task<PageListResponse<string>> GetHealthConditionTypesAsync(int page = 1, int pageSize = 20)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 20;

        var conditions = await _healthConditionRepository.GetAllAsync();
        var types = conditions
            .Select(hc => hc.HealthConditionType)
            .Distinct()
            .ToList();

        var totalCount = types.Count;

        var paginatedItems = types
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PageListResponse<string>
        {
            Items = paginatedItems,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            HasNextPage = (page * pageSize) < totalCount,
            HasPreviousPage = page > 1
        };
    }

    public async Task<PageListResponse<HealthConditionResponse>> GetHealthConditionsByTypeAsync(string type, int page = 1, int pageSize = 20)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 20;

        var conditions = await _healthConditionRepository.GetAllAsync(hc => hc.HealthConditionType == type);
        var totalCount = conditions.Count();

        var paginatedItems = conditions
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = _mapper.Map<List<HealthConditionResponse>>(paginatedItems);

        return new PageListResponse<HealthConditionResponse>
        {
            Items = result,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            HasNextPage = (page * pageSize) < totalCount,
            HasPreviousPage = page > 1
        };
    }
}