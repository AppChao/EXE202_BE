using System.Linq.Expressions;
using AutoMapper;
using EXE202_BE.Data.DTOS;
using EXE202_BE.Data.DTOS.Ingredient;
using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;
using EXE202_BE.Service.Interface;

namespace EXE202_BE.Service.Services;

public class IngredientsService : IIngredientsService
{
    private readonly IIngredientsRepository _ingredientsRepository;
    private readonly IMapper _mapper;

    public IngredientsService(
        IIngredientsRepository ingredientsRepository,
        IMapper mapper)
    {
        _ingredientsRepository = ingredientsRepository;
        _mapper = mapper;
    }

    public async Task<PageListResponse<IngredientResponse>> GetIngredientsAsync(string? searchTerm, int page = 1, int pageSize = 20)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 20;

        Expression<Func<Ingredients, bool>>? filter = null;
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            filter = i => i.IngredientName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase);
        }

        var ingredients = await _ingredientsRepository.GetAllAsync(filter);
        var totalCount = ingredients.Count();

        var paginatedItems = ingredients
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = paginatedItems.Select(i => _mapper.Map<IngredientResponse>(i)).ToList();

        return new PageListResponse<IngredientResponse>
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