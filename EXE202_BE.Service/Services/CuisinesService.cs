using System.Linq.Expressions;
using AutoMapper;
using EXE202_BE.Data.DTOS;
using EXE202_BE.Data.DTOS.Cuisine;
using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;
using EXE202_BE.Service.Interface;

namespace EXE202_BE.Service.Services;

public class CuisinesService : ICuisinesService
{
    private readonly ICuisinesRepository _cuisinesRepository;
    private readonly IMapper _mapper;

    public CuisinesService(
        ICuisinesRepository cuisinesRepository,
        IMapper mapper)
    {
        _cuisinesRepository = cuisinesRepository;
        _mapper = mapper;
    }

    public async Task<PageListResponse<CuisineResponse>> GetCuisinesAsync(string? searchTerm, int page = 1, int pageSize = 20)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 20;

        Expression<Func<Cuisines, bool>>? filter = null;
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            filter = c => c.Nation.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                          c.Region.Contains(searchTerm, StringComparison.OrdinalIgnoreCase);
        }

        var cuisines = await _cuisinesRepository.GetAllAsync(filter);
        var totalCount = cuisines.Count();

        var paginatedItems = cuisines
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = paginatedItems.Select(c => _mapper.Map<CuisineResponse>(c)).ToList();

        return new PageListResponse<CuisineResponse>
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