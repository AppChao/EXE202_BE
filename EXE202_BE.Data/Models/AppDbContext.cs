namespace EXE202_BE.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext<IdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {

        builder.Entity<IdentityRole>().HasData(
            new IdentityRole { Id = "1", Name = "Admin", NormalizedName = "ADMIN" },
            new IdentityRole { Id = "2", Name = "Staff", NormalizedName = "STAFF" },
            new IdentityRole { Id = "3", Name = "Member", NormalizedName = "MEMBER" },
            new IdentityRole { Id = "4", Name = "User", NormalizedName = "USER" }
        );
        
        builder.Entity<RecipeMealTypes>()
            .HasKey(re => new { re.MealId, re.RecipeId });
        
        builder.Entity<RecipeHealthTags>()
            .HasKey(rh => new { rh.RecipeId, rh.HealthTagId });

        builder.Entity<Servings>()
            .HasKey(se => new {se.RecipeId, se.IngredientId});

        builder.Entity<Allergies>()
            .HasKey(al => new { al.IngredientId, al.UPId });

        builder.Entity<PersonalHealthConditions>()
            .HasKey(phc => new {phc.UPId, phc.HealthConditionId});
            
        builder.Entity<Recipes>()
            .ToTable(tb => tb.HasCheckConstraint("CK_Recipes_Difficulty_Rating", "[DifficultyEstimation] BETWEEN 1 AND 10"));
        
        base.OnModelCreating(builder);
    }
}