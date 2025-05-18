using EXE202_BE.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EXE202_BE.Data
{
    public static class SeedUsers
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ModifyIdentityUser>>();
            var loggerFactory = scope.ServiceProvider.GetRequiredService<ILoggerFactory>();
            var logger = loggerFactory.CreateLogger("SeedUsers");

            var users = new[]
            {
                new { Email = "a@gmail.com", Password = "Abcd@1234", Role = "Admin" },
                new { Email = "b@gmail.com", Password = "Abcd@1234", Role = "Staff" }
            };

            foreach (var userData in users)
            {
                var existingUser = await userManager.FindByEmailAsync(userData.Email);
                if (existingUser == null)
                {
                    var user = new ModifyIdentityUser
                    {
                        UserName = userData.Email,
                        Email = userData.Email
                        // Add other custom fields here if you have them
                    };

                    var result = await userManager.CreateAsync(user, userData.Password);
                    if (result.Succeeded)
                    {
                        await userManager.AddToRoleAsync(user, userData.Role);
                        logger.LogInformation("Seeded user {Email} with role {Role}", userData.Email, userData.Role);
                    }
                    else
                    {
                        logger.LogError("Failed to seed user {Email}: {Errors}", userData.Email, string.Join(", ", result.Errors.Select(e => e.Description)));
                    }
                }
            }
        }
    }
}