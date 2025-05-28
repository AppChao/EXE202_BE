using EXE202_BE.Data.DTOS.Ingredient;
using EXE202_BE.Data.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EXE202_BE.Data.SeedData
{
    public static class SeedIngredients
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("SeedIngredients");

            if (context.Ingredients.Any())
            {
                logger.LogInformation("🟡 Ingredients already exist in the database. Skipping seeding.");
                return;
            }

            try
            {
                var baseDir = AppContext.BaseDirectory;
                var projectRoot = Directory.GetParent(baseDir)!.Parent!.Parent!.Parent!.Parent!.FullName;
                var seedFolder = Path.Combine(projectRoot, "EXE202_BE.Data/JsonDataFile");

                if (!Directory.Exists(seedFolder))
                {
                    logger.LogError("❌ Seed data folder not found: {Path}", seedFolder);
                    return;
                }

                var jsonFiles = Directory.GetFiles(seedFolder, "*.json");

                var allIngredients = new List<Ingredients>();

                foreach (var file in jsonFiles)
                {
                    var json = await File.ReadAllTextAsync(file);
                    var ingredientDtos = JsonConvert.DeserializeObject<List<IngredientDto>>(json);

                    if (ingredientDtos != null)
                        allIngredients.AddRange(ingredientDtos.Select(dto => dto.ToEntity()));
                }

                if (allIngredients.Count > 0)
                {
                    await context.Ingredients.AddRangeAsync(allIngredients);
                    await context.SaveChangesAsync();
                    logger.LogInformation("✅ Seeded {Count} ingredients from {FileCount} file(s).", allIngredients.Count, jsonFiles.Length);
                }
                else
                {
                    logger.LogWarning("⚠️ No ingredients found in JSON files.");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "❌ An error occurred while seeding ingredients.");
            }
        }
    }
}
