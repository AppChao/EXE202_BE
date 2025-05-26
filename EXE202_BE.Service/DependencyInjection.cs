using EXE202_BE.Data.Models;
using EXE202_BE.Service.Interface;
using EXE202_BE.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace EXE202_BE.Service;

public static class DependencyInjection
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IAllergiesService, AllergiesService>();
        services.AddTransient<IHealthConditionsService, HealthConditionsService>();
        services.AddTransient<IHealthTagsService, HealthTagsService>();
        services.AddTransient<IMealCatagoriesService, MealCatagoriesService>();
        services.AddTransient<IPersonalHealthConditionsService, PersonalHealthConditionsService>();
        services.AddTransient<ISubcriptionsService, SubcriptionsService>();
        services.AddTransient<IRecipeHealthTagsService, RecipeHealthTagsService>();
        services.AddTransient<IRecipeMealTypesService, RecipeMealTypesService>();
        services.AddTransient<IRecipesService, RecipesService>();
        services.AddTransient<IServingsService, ServingsService>();
        services.AddTransient<IUserProfilesService, UserProfilesService>();
        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IDashboardService, DashboardService>();
        services.AddTransient<INotificationService, NotificationsService>();
        services.AddTransient<IIngredientsService, IngredientsService>();
        services.AddTransient<ICuisinesService, CuisinesService>();
        services.AddTransient<IHealthTagsService, HealthTagsService>();
        services.AddTransient<IMealCatagoriesService, MealCatagoriesService>();
        services.AddTransient<IEmailSender<ModifyIdentityUser>, SmtpEmailSender>();
        
        return services;
    }
}