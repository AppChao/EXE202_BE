namespace EXE202_BE.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext<ModifyIdentityUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public virtual DbSet<ActivityLevels> ActivityLevels { get; set; }
    
    public virtual DbSet<Allergies> Allergies { get; set; }
    
    public virtual DbSet<CookingSkills> CookingSkills { get; set; }
    
    public virtual DbSet<Cuisines> Cuisines { get; set; }
    
    public virtual DbSet<Devices> Devices { get; set; }
    
    public virtual DbSet<Goals> Goals { get; set; }
    
    public virtual DbSet<HealthConditions> HealthConditions { get; set; }
    
    public virtual DbSet<HealthTags> HealthTags { get; set; }
    
    public virtual DbSet<Ingredients> Ingredients { get; set; }
    
    public virtual DbSet<IngredientTypes> IngredientTypes { get; set; }
    
    public virtual DbSet<LoseWeightSpeed> LoseWeightSpeed { get; set; }
    
    public virtual DbSet<MealCatagories> MealCatagories { get; set; }
    
    public virtual DbSet<MealScheduled> MealScheduled { get; set; }
    
    public virtual DbSet<Notifications> Notifications { get; set; }
    
    public virtual DbSet<NotificationUsers> NotificationUsers { get; set; }
    
    public virtual DbSet<PersonalHealthConditions> PersonalHealthConditions { get; set; }
    
    public virtual DbSet<PersonalUserCookingSkills> PersonalUserCookingSkills { get; set; }
    
    public virtual DbSet<PersonalUserProblem> PersonalUserProblem { get; set; }
    
    public virtual DbSet<RecipeHealthTags> RecipeHealthTags { get; set; }
    
    public virtual DbSet<RecipeMealTypes> RecipeMealTypes { get; set; }
    
    public virtual DbSet<Recipes> Recipes { get; set; }
    
    public virtual DbSet<Servings> Servings { get; set; }
    
    public virtual DbSet<Subcriptions> Subcriptions { get; set; }
    
    public virtual DbSet<UserExperiences> UserExperiences { get; set; }
    
    public virtual DbSet<UserProblem> UserProblem { get; set; }
    
    public virtual DbSet<UserProfiles> UserProfiles { get; set; }

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
            .HasKey(se => new { se.RecipeId, se.IngredientId });

        builder.Entity<Allergies>()
            .HasKey(al => new { al.IngredientId, al.UPId });

        builder.Entity<PersonalHealthConditions>()
            .HasKey(phc => new { phc.UPId, phc.HealthConditionId });
        
        builder.Entity<PersonalUserProblem>()
            .HasKey(pup => new { pup.UPId, pup.ProblemId });
        
        builder.Entity<PersonalUserCookingSkills>()
            .HasKey(pucs => new { pucs.UPId, pucs.CookingSkillId });
        
        builder.Entity<NotificationUsers>()
            .HasKey(nu => new {nu.UserId, nu.NotificationId});

        builder.Entity<Recipes>()
            .ToTable(tb => tb.HasCheckConstraint("CK_Recipes_Difficulty_Rating", "\"DifficultyEstimation\" BETWEEN 1 AND 5"));

        builder.Entity<Recipes>()
            .ToTable(tb => tb.HasCheckConstraint("CK_Recipes_Meals", "\"Meals\" IN ('breakfast', 'lunch', 'dinner', 'snack')"));

        builder.Entity<UserProfiles>()
            .ToTable(tb => tb.HasCheckConstraint("CK_UserProfiles_Gender", "\"Gender\" IN ('Male', 'Female', 'Other')"));
        
        // Thêm từ đây - Cấu hình tối thiểu để sửa lỗi MappingProfile
        builder.Entity<UserProfiles>()
            .HasMany(up => up.Allergies)
            .WithOne(a => a.UserProfile)
            .HasForeignKey(a => a.UPId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<UserProfiles>()
            .HasMany(up => up.PersonalHealthConditions)
            .WithOne(phc => phc.UserProfile)
            .HasForeignKey(phc => phc.UPId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Allergies>()
            .HasOne(a => a.Ingredient)
            .WithMany()
            .HasForeignKey(a => a.IngredientId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<PersonalHealthConditions>()
            .HasOne(phc => phc.HealthCondition)
            .WithMany()
            .HasForeignKey(phc => phc.HealthConditionId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<UserProfiles>()
            .HasOne(up => up.User)
            .WithOne()
            .HasForeignKey<UserProfiles>(up => up.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        
        base.OnModelCreating(builder);
    }
}