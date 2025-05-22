using System.Linq.Expressions;
using AutoMapper;
using EXE202_BE.Data.DTOS;
using EXE202_BE.Data.DTOS.MealCategory;
using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;
using EXE202_BE.Service.Interface;

namespace EXE202_BE.Service.Services;

public class MealCatagoriesService : IMealCatagoriesService
{
    private readonly IMealCatagoriesRepository _mealCatagoriesRepository;
    private readonly IMapper _mapper;

    public MealCatagoriesService(
        IMealCatagoriesRepository mealCatagoriesRepository,
        IMapper mapper)
    {
        _mealCatagoriesRepository = mealCatagoriesRepository;
        _mapper = mapper;
    }

    public async Task<PageListResponse<MealCategoryResponse>> GetMealCategoriesAsync(string? searchTerm, int page = 1, int pageSize = 20)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 20;

        Expression<Func<MealCatagories, bool>>? filter = null;
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            filter = m => m.MealName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase);
        }

        var mealCategories = await _mealCatagoriesRepository.GetAllAsync(filter);
        var totalCount = mealCategories.Count();

        var paginatedItems = mealCategories
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = paginatedItems.Select(m => _mapper.Map<MealCategoryResponse>(m)).ToList();

        return new PageListResponse<MealCategoryResponse>
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