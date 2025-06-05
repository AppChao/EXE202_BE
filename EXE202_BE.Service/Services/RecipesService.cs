using System.Linq.Expressions;
using System.Text.Json;
using AutoMapper;
using EXE202_BE.Data.DTOS;
using EXE202_BE.Data.DTOS.Recipe;
using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;
using EXE202_BE.Service.Interface;

namespace EXE202_BE.Service.Services;

public class RecipesService : IRecipesService
{
    private readonly IRecipesRepository _recipesRepository;
    private readonly IServingsRepository _servingsRepository;
    private readonly IRecipeHealthTagsRepository _recipeHealthTagsRepository;
    private readonly IRecipeMealTypesRepository _recipeMealTypesRepository;
    private readonly ICuisinesRepository _cuisinesRepository;
    private readonly IIngredientsRepository _ingredientsRepository;
    private readonly IHealthTagsRepository _healthTagsRepository;
    private readonly IMealCatagoriesRepository _mealCatagoriesRepository;
    private readonly IMapper _mapper;

    public RecipesService(
        IRecipesRepository recipesRepository,
        IServingsRepository servingsRepository,
        IRecipeHealthTagsRepository recipeHealthTagsRepository,
        IRecipeMealTypesRepository recipeMealTypesRepository,
        ICuisinesRepository cuisinesRepository,
        IIngredientsRepository ingredientsRepository,
        IHealthTagsRepository healthTagsRepository,
        IMealCatagoriesRepository mealCatagoriesRepository,
        IMapper mapper)
    {
        _recipesRepository = recipesRepository;
        _servingsRepository = servingsRepository;
        _recipeHealthTagsRepository = recipeHealthTagsRepository;
        _recipeMealTypesRepository = recipeMealTypesRepository;
        _cuisinesRepository = cuisinesRepository;
        _ingredientsRepository = ingredientsRepository;
        _healthTagsRepository = healthTagsRepository;
        _mealCatagoriesRepository = mealCatagoriesRepository;
        _mapper = mapper;
    }

    public async Task<PageListResponse<RecipeResponse>> GetRecipesAsync(string? searchTerm, int page = 1, int pageSize = 20)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 20;

        Expression<Func<Recipes, bool>>? filter = null;
        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            filter = r => r.RecipeName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                          r.Meals.Contains(searchTerm, StringComparison.OrdinalIgnoreCase);
        }

        var recipes = await _recipesRepository.GetAllAsync(filter, "Cuisine");

        var totalCount = recipes.Count();

        var paginatedRecipes = recipes
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = paginatedRecipes.Select(r => _mapper.Map<RecipeResponse>(r)).ToList();

        return new PageListResponse<RecipeResponse>
        {
            Items = result,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            HasNextPage = (page * pageSize) < totalCount,
            HasPreviousPage = page > 1
        };
    }

    public async Task<RecipeResponse> GetRecipeByIdAsync(int id)
    {
        var recipe = await _recipesRepository.GetAsync(
            r => r.RecipeId == id,
            "Cuisine,Servings.Ingredient");

        if (recipe == null)
        {
            throw new Exception("Recipe not found.");
        }

        var response = _mapper.Map<RecipeResponse>(recipe);
        response.Steps = ParseRecipeSteps(recipe.RecipeSteps);
        return response;
    }

    public async Task<RecipeResponse> CreateRecipeAsync(RecipeRequest request)
    {
        // Kiểm tra validation
        if (string.IsNullOrWhiteSpace(request.RecipeName))
        {
            throw new ArgumentException("Recipe name is required.");
        }

        if (request.CuisineId <= 0)
        {
            throw new ArgumentException("Cuisine ID is required.");
        }

        var cuisine = await _cuisinesRepository.GetAsync(c => c.CuisineId == request.CuisineId);
        if (cuisine == null)
        {
            throw new ArgumentException($"Invalid cuisine ID: {request.CuisineId}.");
        }

        // Parse steps to JSON
        string recipeStepsJson = JsonSerializer.Serialize(request.Steps);

        // Tạo recipe
        var recipe = new Recipes
        {
            RecipeName = request.RecipeName,
            CuisineId = request.CuisineId,
            Meals = request.Meals,
            RecipeSteps = recipeStepsJson,
            InstructionVideoLink = request.InstructionVideoLink,
            TimeEstimation = request.TimeEstimation,
            DifficultyEstimation = request.DifficultyEstimation
        };

        await _recipesRepository.AddAsync(recipe);

        // Thêm Servings
        foreach (var ingredient in request.Ingredients)
        {
            var ingredientEntity = await _ingredientsRepository.GetAsync(i => i.IngredientName == ingredient.Ingredient);
            if (ingredientEntity == null)
            {
                throw new ArgumentException($"Invalid ingredient: {ingredient.Ingredient}.");
            }

            var servingEntity = new Servings
            {
                RecipeId = recipe.RecipeId,
                IngredientId = ingredientEntity.IngredientId,
                Ammount = $"{ingredient.Amount}"
            };
            await _servingsRepository.AddAsync(servingEntity);
        }

        var createdRecipe = await _recipesRepository.GetAsync(
            r => r.RecipeId == recipe.RecipeId,
            "Cuisine,Servings.Ingredient");

        var response = _mapper.Map<RecipeResponse>(createdRecipe);
        response.Steps = ParseRecipeSteps(createdRecipe.RecipeSteps);
        return response;
    }

public async Task<RecipeResponse> UpdateRecipeAsync(int id, RecipeRequest request)
{
    var recipe = await _recipesRepository.GetAsync(r => r.RecipeId == id);
    if (recipe == null)
    {
        throw new Exception("Recipe not found.");
    }

    // Kiểm tra validation
    if (string.IsNullOrWhiteSpace(request.RecipeName))
    {
        throw new ArgumentException("Recipe name is required.");
    }

    if (request.CuisineId <= 0)
    {
        throw new ArgumentException("Cuisine ID is required.");
    }

    var cuisine = await _cuisinesRepository.GetAsync(c => c.CuisineId == request.CuisineId);
    if (cuisine == null)
    {
        throw new ArgumentException($"Invalid cuisine ID: {request.CuisineId}.");
    }

    if (request.Ingredients == null || !request.Ingredients.Any())
    {
        throw new ArgumentException("At least one ingredient is required.");
    }

    if (request.Steps == null || !request.Steps.Any())
    {
        throw new ArgumentException("At least one step is required.");
    }

    foreach (var ingredient in request.Ingredients)
    {
        if (string.IsNullOrWhiteSpace(ingredient.Ingredient))
        {
            throw new ArgumentException("Ingredient name is required.");
        }
        if (string.IsNullOrWhiteSpace(ingredient.Amount) || !double.TryParse(ingredient.Amount, out var amount) || amount <= 0)
        {
            throw new ArgumentException($"Invalid amount for ingredient '{ingredient.Ingredient}': must be a positive number.");
        }
    }

    foreach (var step in request.Steps)
    {
        if (string.IsNullOrWhiteSpace(step.Instruction))
        {
            throw new ArgumentException("All step instructions must be provided.");
        }
    }

    // Parse steps to JSON
    string recipeStepsJson = JsonSerializer.Serialize(request.Steps);

    // Bắt đầu giao dịch
    using var transaction = await _recipesRepository.GetDbContext().Database.BeginTransactionAsync();

    try
    {
        // Cập nhật recipe
        recipe.RecipeName = request.RecipeName;
        recipe.CuisineId = request.CuisineId;
        recipe.Meals = request.Meals;
        recipe.RecipeSteps = recipeStepsJson;
        recipe.InstructionVideoLink = request.InstructionVideoLink;
        recipe.TimeEstimation = request.TimeEstimation;
        recipe.DifficultyEstimation = request.DifficultyEstimation;

        await _recipesRepository.UpdateAsync(recipe);

        // Xóa Servings cũ
        var oldServings = await _servingsRepository.GetAllAsync(s => s.RecipeId == id);
        foreach (var serving in oldServings)
        {
            await _servingsRepository.DeleteAsync(serving);
        }

        // Thêm Servings mới
        foreach (var ingredient in request.Ingredients)
        {
            var ingredientEntity = await _ingredientsRepository.GetAsync(i => i.IngredientName == ingredient.Ingredient);
            if (ingredientEntity == null)
            {
                throw new ArgumentException($"Invalid ingredient: {ingredient.Ingredient}.");
            }

            var servingEntity = new Servings
            {
                RecipeId = id,
                IngredientId = ingredientEntity.IngredientId,
                Ammount = ingredient.Amount // Chỉ lưu Amount, không gộp DefaultUnit
            };
            await _servingsRepository.AddAsync(servingEntity);
        }

        // Commit giao dịch
        await transaction.CommitAsync();

        var updatedRecipe = await _recipesRepository.GetAsync(
            r => r.RecipeId == id,
            "Cuisine,Servings.Ingredient");

        var response = _mapper.Map<RecipeResponse>(updatedRecipe);
        response.Steps = ParseRecipeSteps(updatedRecipe.RecipeSteps);
        return response;
    }
    catch (Exception)
    {
        await transaction.RollbackAsync();
        throw;
    }
}

    public async Task DeleteRecipeAsync(int id)
    {
        var recipe = await _recipesRepository.GetAsync(r => r.RecipeId == id);
        if (recipe == null)
        {
            throw new Exception("Recipe not found.");
        }

        // Xóa Servings
        var servings = await _servingsRepository.GetAllAsync(s => s.RecipeId == id);
        foreach (var serving in servings)
        {
            await _servingsRepository.DeleteAsync(serving);
        }

        await _recipesRepository.DeleteAsync(recipe);
    }
    
    private List<RecipeStep> ParseRecipeSteps(string? recipeSteps)
    {
        if (string.IsNullOrEmpty(recipeSteps))
        {
            return new List<RecipeStep>();
        }

        try
        {
            return JsonSerializer.Deserialize<List<RecipeStep>>(recipeSteps) ?? new List<RecipeStep>();
        }
        catch
        {
            return new List<RecipeStep>();
        }
    }
    
    public async Task<PageListResponse<RecipeHomeResponse>> GetRecipesHomeAsync(string? category, int page = 1, int pageSize = 14)
    {
        if (page < 1) page = 1;
        if (pageSize < 1) pageSize = 14;

        var recipes = await _recipesRepository.GetRecipesByCategoryAsync(category);
        var totalCount = recipes.Count;

        var paginatedItems = recipes
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        var result = _mapper.Map<List<RecipeHomeResponse>>(paginatedItems);

        return new PageListResponse<RecipeHomeResponse>
        {
            Items = result,
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            HasNextPage = (page * pageSize) < totalCount,
            HasPreviousPage = page > 1
        };
    }

    /*public async Task DeleteRecipeAsync(int id)
    {
        var recipe = await _recipesRepository.GetAsync(r => r.RecipeId == id);
        if (recipe == null)
        {
            throw new Exception("Recipe not found.");
        }

        // Xóa các bảng liên quan
        var servings = await _servingsRepository.GetAllAsync(s => s.RecipeId == id);
        foreach (var serving in servings)
        {
            await _servingsRepository.DeleteAsync(serving);
        }

        var healthTags = await _recipeHealthTagsRepository.GetAllAsync(rht => rht.RecipeId == id);
        foreach (var healthTag in healthTags)
        {
            await _recipeHealthTagsRepository.DeleteAsync(healthTag);
        }

        var mealTypes = await _recipeMealTypesRepository.GetAllAsync(rmt => rmt.RecipeId == id);
        foreach (var mealType in mealTypes)
        {
            await _recipeMealTypesRepository.DeleteAsync(mealType);
        }

        await _recipesRepository.DeleteAsync(recipe);
    }*/
}