using EXE202_BE.Repository.Interface;
using EXE202_BE.Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace EXE202_BE.Repository;

public static class DependencyInjection
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddTransient<IAllergiesRepository, AllergiesRepository>();
        services.AddTransient<IHealthConditionsRepository, HealthConditionsRepository>();
        services.AddTransient<IHealthTagsRepository, HealthTagsRepository>();
        services.AddTransient<IMealCatagoriesRepository, MealCatagoriesRepository>();
        services.AddTransient<IPersonalHealthConditionsRepository, PersonalHealthConditionsRepository>();
        services.AddTransient<IRecipeHealthTagsRepository, RecipeHealthTagsRepository>();
        services.AddTransient<IRecipeMealTypesRepository, RecipeMealTypesRepository>();
        services.AddTransient<IRecipesRepository, RecipesRepository>();
        services.AddTransient<IServingsRepository, ServingsRepository>();
        services.AddTransient<ISubcriptionsRepository, SubcriptionsRepository>();
        services.AddTransient<IUserProfilesRepository, UserProfilesRepository>();
        services.AddTransient<INotificationsRepository, NotificationsRepository>();
        services.AddTransient<IDevicesRepository, DevicesRepository>();
        services.AddTransient<IIngredientsRepository, IngredientsRepository>(); 
        services.AddTransient<ICuisinesRepository, CuisinesRepository>(); 
        services.AddTransient<IHealthTagsRepository, HealthTagsRepository>(); 
        services.AddTransient<IMealCatagoriesRepository, MealCatagoriesRepository>();
        return services;
    }
}