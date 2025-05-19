using AutoMapper;
using EXE202_BE.Data.Models;
using System.Collections.Generic;
using EXE202_BE.Data.DTOS.Dashboard;

namespace EXE202_BE.Data.DTOS.User;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UserProfiles, UserProfileResponse>()
            .ForMember(dest => dest.UPId, opt => opt.MapFrom(src => src.UPId))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.Age, opt => opt.MapFrom(src => src.Age))
            .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => src.Gender))
            .ForMember(dest => dest.Allergies, opt => opt.MapFrom(src => src.Allergies.Select(a => a.Ingredient.IngredientName).ToList()))
            .ForMember(dest => dest.HealthConditions, opt => opt.MapFrom(src => src.PersonalHealthConditions.Select(h => h.HealthCondition.HealthConditionName).ToList()))
            .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.Role, opt => opt.Ignore());

        CreateMap<UserProfiles, AdminProfileResponse>()
            .ForMember(dest => dest.UPId, opt => opt.MapFrom(src => src.UPId))
            .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
            .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.User.Email))
            .ForMember(dest => dest.PhoneNumber, opt => opt.MapFrom(src => src.User.PhoneNumber))
            .ForMember(dest => dest.Role, opt => opt.Ignore());
        CreateMap<Recipes, TopRecipesResponse>()
            .ForMember(dest => dest.RecipeId, opt => opt.MapFrom(src => src.RecipeId))
            .ForMember(dest => dest.RecipeName, opt => opt.MapFrom(src => src.RecipeName))
            .ForMember(dest => dest.SelectionCount, opt => opt.Ignore());
    }
}