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
    private readonly IIngredientsTypeRepository _ingredientsTypeRepository;
    private readonly IMapper _mapper; 

    public IngredientsService(
        IIngredientsRepository ingredientsRepository,
        IIngredientsTypeRepository ingredientsTypeRepository,
        IMapper mapper)
    {
        _ingredientsRepository = ingredientsRepository;
        _mapper = mapper;
        _ingredientsTypeRepository = ingredientsTypeRepository;
    }

    public async Task<PageListResponse<Ingredient1Response>> GetIngredientsAsync(string? searchTerm, int page = 1, int pageSize = 20)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 20;

        Expression<Func<Ingredients, bool>>? filter = null;
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            filter = i => i.IngredientName.ToLower().Contains(searchTerm.ToLower());
        }

        var ingredients = await _ingredientsRepository.GetAllAsync(filter);
        var totalCount = ingredients.Count();

        var paginatedItems = ingredients
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = paginatedItems.Select(i => _mapper.Map<Ingredient1Response>(i)).ToList();

        return new PageListResponse<Ingredient1Response>
        {
            Items = result,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            HasNextPage = (page * pageSize) < totalCount,
            HasPreviousPage = page > 1
        };
    }
    
    public async Task<PageListResponse<IngredientTypeResponse>> GetIngredientTypesAsync(string? searchTerm, int page = 1, int pageSize = 20)
    {
        var response = await _ingredientsTypeRepository.GetIngredientTypesAsync(searchTerm, page, pageSize);
        var result = _mapper.Map<List<IngredientTypeResponse>>(response.Items);

        return new PageListResponse<IngredientTypeResponse>
        {
            Items = result,
            Page = response.Page,
            PageSize = response.PageSize,
            TotalCount = response.TotalCount,
            HasNextPage = response.HasNextPage,
            HasPreviousPage = response.HasPreviousPage
        };
    }

    public async Task<PageListResponse<IngredientResponse>> GetIngredientsByTypeAsync(int? typeId, string? searchTerm, int page = 1, int pageSize = 20)
    {
        var response = await _ingredientsRepository.GetIngredientsAsync(typeId, searchTerm, page, pageSize);
        var result = _mapper.Map<List<IngredientResponse>>(response.Items);

        return new PageListResponse<IngredientResponse>
        {
            Items = result,
            Page = response.Page,
            PageSize = response.PageSize,
            TotalCount = response.TotalCount,
            HasNextPage = response.HasNextPage,
            HasPreviousPage = response.HasPreviousPage
        };
    }

    public async Task<List<CommonAllergenResponse>?>? GetCommonAllergensAsync()
    {
        var allergens = await _ingredientsRepository.GetAllIngredientsOrderByIconAsync();

        if (allergens == null)
        {
            return null;
        }
        
        List<CommonAllergenResponse>? result = new List<CommonAllergenResponse>();
        foreach (var ingredient in allergens)
        {
                result.Add(_mapper.Map<CommonAllergenResponse>(ingredient));
        }

        return result;
    }
}