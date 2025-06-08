using AutoMapper;
using EXE202_BE.Data.Models;
using System.Collections.Generic;
using System.Text.Json;
using EXE202_BE.Data.DTOS.Cuisine;
using EXE202_BE.Data.DTOS.Dashboard;
using EXE202_BE.Data.DTOS.Goals;
using EXE202_BE.Data.DTOS.HealthTag;
using EXE202_BE.Data.DTOS.Ingredient;
using EXE202_BE.Data.DTOS.MealCategory;
using EXE202_BE.Data.DTOS.Recipe;
using IngredientDetail = EXE202_BE.Data.DTOS.Ingredient.IngredientDetail;

namespace EXE202_BE.Data.DTOS.User;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserProfiles, UserProfileResponse>()
            .ForMember(dest => dest.UPId, opt => opt.MapFrom(src => src.UPId))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName ?? string.Empty))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName ?? string.Empty))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender ?? string.Empty))
            .ForMember(dest => dest.Allergies, opt => opt.MapFrom(src => src.Allergies.Select(a => a.Ingredient.IngredientName ?? string.Empty).ToList()))
            .ForMember(dest => dest.HealthConditions, opt => opt.MapFrom(src => src.PersonalHealthConditions.Select(h => new HealthConditionDTO
            {
                Condition = h.HealthCondition.HealthConditionName ?? string.Empty,
                Status = h.Status ?? string.Empty
            }).ToList()))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId ?? string.Empty))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email ?? string.Empty))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
            .ForMember(dest => dest.SubcriptionId, opt => opt.MapFrom(src => src.SubcriptionId))
            .ForMember(dest => dest.Role, opt => opt.Ignore())
            .ForMember(dest => dest.UserPicture, opt => opt.MapFrom(src => src.UserPicture != null ? src.UserPicture.ToString() : string.Empty))
            .ForMember(dest => dest.EndDate, opt => opt.MapFrom(src => src.EndDate))
            .ForMember(dest => dest.Streak, opt => opt.MapFrom(src => src.Streak));

        CreateMap<UpdateUserProfileRequestDTO, UserProfiles>()
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Allergies, opt => opt.Ignore()) // Bỏ qua vì được xử lý riêng trong service
            .ForMember(dest => dest.PersonalHealthConditions, opt => opt.Ignore()) // Bỏ qua vì được xử lý riêng trong service
            .ForMember(dest => dest.UPId, opt => opt.Ignore())
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.SubcriptionId, opt => opt.Ignore())
            .ForMember(dest => dest.UserPicture, opt => opt.Ignore())
            .ForMember(dest => dest.Weight, opt => opt.Ignore())
            .ForMember(dest => dest.GoalWeight, opt => opt.Ignore())
            .ForMember(dest => dest.Height, opt => opt.Ignore())
            .ForMember(dest => dest.GoalId, opt => opt.Ignore())
            .ForMember(dest => dest.ExperienceId, opt => opt.Ignore())
            .ForMember(dest => dest.LevelId, opt => opt.Ignore())
            .ForMember(dest => dest.SpeedId, opt => opt.Ignore())
            .ForMember(dest => dest.StartDate, opt => opt.Ignore())
            .ForMember(dest => dest.EndDate, opt => opt.Ignore());

        CreateMap<UserProfiles, AdminProfileResponse>()
            .ForMember(dest => dest.UPId, opt => opt.MapFrom(src => src.UPId))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
            .ForMember(dest => dest.Role, opt => opt.Ignore())
            .ForMember(dest => dest.UserPicture, opt => opt.MapFrom(src => src.UserPicture.ToString()));

        CreateMap<Recipes, TopRecipesResponse>()
            .ForMember(dest => dest.RecipeId, opt => opt.MapFrom(src => src.RecipeId))
            .ForMember(dest => dest.RecipeName, opt => opt.MapFrom(src => src.RecipeName))
            .ForMember(dest => dest.SelectionCount, opt => opt.Ignore());

        CreateMap<Recipes, RecipeResponse>()
            .ForMember(dest => dest.RecipeId, opt => opt.MapFrom(src => src.RecipeId))
            .ForMember(dest => dest.RecipeName, opt => opt.MapFrom(src => src.RecipeName))
            .ForMember(dest => dest.Meals, opt => opt.MapFrom(src => src.Meals))
            .ForMember(dest => dest.DifficultyEstimation, opt => opt.MapFrom(src => src.DifficultyEstimation))
            .ForMember(dest => dest.TimeEstimation, opt => opt.MapFrom(src => src.TimeEstimation))
            .ForMember(dest => dest.Nation, opt => opt.MapFrom(src => src.Cuisine.Nation))
            .ForMember(dest => dest.CuisineId, opt => opt.MapFrom(src => src.CuisineId))
            .ForMember(dest => dest.InstructionVideoLink, opt => opt.MapFrom(src => src.InstructionVideoLink))
            .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src => src.Servings.Select(s => new EXE202_BE.Data.DTOS.Recipe.IngredientDetail
            {
                Ingredient = s.Ingredient.IngredientName,
                Amount = s.Ammount,
                DefaultUnit = s.Ingredient.DefaultUnit
            }).ToList()))
            .ForMember(dest => dest.Steps, opt => opt.Ignore())
            .ForMember(dest => dest.RecipeSteps, opt => opt.MapFrom(src => src.RecipeSteps));

        CreateMap<Recipes, RecipeHomeResponse>()
            .ForMember(dest => dest.RecipeId, opt => opt.MapFrom(src => src.RecipeId))
            .ForMember(dest => dest.RecipeName, opt => opt.MapFrom(src => src.RecipeName))
            .ForMember(dest => dest.TimeEstimation, opt => opt.MapFrom(src => src.TimeEstimation))
            .ForMember(dest => dest.DifficultyEstimation, opt => opt.MapFrom(src => src.DifficultyEstimation))
            .ForMember(dest => dest.MealName, opt => opt.MapFrom(src => src.Meals ?? string.Empty))
            .ForMember(dest => dest.ImageUrl, opt => opt.MapFrom(src => string.Empty));

        CreateMap<IngredientTypes, IngredientTypeResponse>()
            .ForMember(dest => dest.IngredientTypeId, opt => opt.MapFrom(src => src.IngredientTypeId))
            .ForMember(dest => dest.TypeName, opt => opt.MapFrom(src => src.TypeName));

        CreateMap<Ingredients, IngredientResponse>()
            .ForMember(dest => dest.IngredientId, opt => opt.MapFrom(src => src.IngredientId))
            .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.IngredientName))
            .ForMember(dest => dest.DefaultUnit, opt => opt.MapFrom(src => src.DefaultUnit));

        CreateMap<HealthConditions, HealthConditionResponse>()
            .ForMember(dest => dest.HealthConditionId, opt => opt.MapFrom(src => src.HealthConditionId))
            .ForMember(dest => dest.HealthConditionName, opt => opt.MapFrom(src => src.HealthConditionName))
            .ForMember(dest => dest.BriefDescription, opt => opt.MapFrom(src => src.BriefDescription))
            .ForMember(dest => dest.HealthConditionType, opt => opt.MapFrom(src => src.HealthConditionType));

        CreateMap<Ingredients, Ingredient1Response>()
            .ForMember(dest => dest.IngredientId, opt => opt.MapFrom(src => src.IngredientId))
            .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.IngredientName))
            .ForMember(dest => dest.DefaultUnit, opt => opt.MapFrom(src => src.DefaultUnit));

        CreateMap<Cuisines, CuisineResponse>()
            .ForMember(dest => dest.CuisineId, opt => opt.MapFrom(src => src.CuisineId))
            .ForMember(dest => dest.Nation, opt => opt.MapFrom(src => src.Nation))
            .ForMember(dest => dest.Region, opt => opt.MapFrom(src => src.Region));

        CreateMap<HealthTags, HealthTagResponse>()
            .ForMember(dest => dest.HealthTagId, opt => opt.MapFrom(src => src.HealthTagId))
            .ForMember(dest => dest.HealthTagName, opt => opt.MapFrom(src => src.HealthTagName));

        CreateMap<MealCatagories, MealCategoryResponse>()
            .ForMember(dest => dest.MealId, opt => opt.MapFrom(src => src.MealId))
            .ForMember(dest => dest.MealName, opt => opt.MapFrom(src => src.MealName));

        CreateMap<Models.Goals, GoalResponse>()
            .ForMember(dest => dest.GoalId, opt => opt.MapFrom(src => src.GoalId))
            .ForMember(dest => dest.GoalName, opt => opt.MapFrom(src => src.GoalName));

        CreateMap<Models.Ingredients, CommonAllergenResponse>()
            .ForMember(dest => dest.IngredientId, opt => opt.MapFrom(src => src.IngredientId))
            .ForMember(dest => dest.IngredientName, opt => opt.MapFrom(src => src.IngredientName))
            .ForMember(dest => dest.DefaultUnit, opt => opt.MapFrom(src => src.DefaultUnit))
            .ForMember(dest => dest.IconLibrary, opt => opt.MapFrom(src => src.IconLibrary))
            .ForMember(dest => dest.IconName, opt => opt.MapFrom(src => src.IconName));

    }
}