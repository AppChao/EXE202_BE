using System.Linq.Expressions;
using AutoMapper;
using EXE202_BE.Data.DTOS;
using EXE202_BE.Data.DTOS.HealthTag;
using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;
using EXE202_BE.Service.Interface;

namespace EXE202_BE.Service.Services;

public class HealthTagsService : IHealthTagsService
{
    private readonly IHealthTagsRepository _healthTagsRepository;
    private readonly IMapper _mapper;

    public HealthTagsService(
        IHealthTagsRepository healthTagsRepository,
        IMapper mapper)
    {
        _healthTagsRepository = healthTagsRepository;
        _mapper = mapper;
    }

    public async Task<PageListResponse<HealthTagResponse>> GetHealthTagsAsync(string? searchTerm, int page = 1, int pageSize = 20)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 20;

        Expression<Func<HealthTags, bool>>? filter = null;
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            filter = ht => ht.HealthTagName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase);
        }

        var healthTags = await _healthTagsRepository.GetAllAsync(filter);
        var totalCount = healthTags.Count();

        var paginatedItems = healthTags
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = paginatedItems.Select(ht => _mapper.Map<HealthTagResponse>(ht)).ToList();

        return new PageListResponse<HealthTagResponse>
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