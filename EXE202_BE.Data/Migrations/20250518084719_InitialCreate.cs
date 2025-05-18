using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EXE202_BE.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ActivityLevels",
                columns: table => new
                {
                    LevelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LevelName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LevelDescription = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLevels", x => x.LevelId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CookingSkills",
                columns: table => new
                {
                    CookingSkillId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CookingSkillName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DifficultyValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CookingSkills", x => x.CookingSkillId);
                });

            migrationBuilder.CreateTable(
                name: "Cuisines",
                columns: table => new
                {
                    CuisineId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Region = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuisines", x => x.CuisineId);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    GoalId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GoalName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.GoalId);
                });

            migrationBuilder.CreateTable(
                name: "HealthConditions",
                columns: table => new
                {
                    HealthConditionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HealthConditionName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthConditions", x => x.HealthConditionId);
                });

            migrationBuilder.CreateTable(
                name: "HealthTags",
                columns: table => new
                {
                    HealthTagId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HealthTagName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthTags", x => x.HealthTagId);
                });

            migrationBuilder.CreateTable(
                name: "IngredientTypes",
                columns: table => new
                {
                    IngredientTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TypeName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientTypes", x => x.IngredientTypeId);
                });

            migrationBuilder.CreateTable(
                name: "LoseWeightSpeed",
                columns: table => new
                {
                    SpeedId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpeedName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeToReachGoal = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoseWeightSpeed", x => x.SpeedId);
                });

            migrationBuilder.CreateTable(
                name: "MealCatagories",
                columns: table => new
                {
                    MealId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MealName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealCatagories", x => x.MealId);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Body = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationId);
                });

            migrationBuilder.CreateTable(
                name: "Subcriptions",
                columns: table => new
                {
                    SubcriptionId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SubcriptionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SubcriptionInfor = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcriptions", x => x.SubcriptionId);
                });

            migrationBuilder.CreateTable(
                name: "UserExperiences",
                columns: table => new
                {
                    ExperienceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExperienceName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExperiences", x => x.ExperienceId);
                });

            migrationBuilder.CreateTable(
                name: "UserProblem",
                columns: table => new
                {
                    ProblemId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProblemName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProblem", x => x.ProblemId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Devices",
                columns: table => new
                {
                    DeviceToken = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Platform = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Devices", x => x.DeviceToken);
                    table.ForeignKey(
                        name: "FK_Devices_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Recipes",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CuisineId = table.Column<int>(type: "int", nullable: true),
                    Meals = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipeSteps = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    InstructionVideoLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RecipeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TimeEstimation = table.Column<int>(type: "int", nullable: false),
                    DifficultyEstimation = table.Column<int>(type: "int", nullable: false),
                    DefaultServing = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.RecipeId);
                    table.CheckConstraint("CK_Recipes_Difficulty_Rating", "[DifficultyEstimation] BETWEEN 1 AND 10");
                    table.CheckConstraint("CK_Recipes_Meals", "[Meals] IN ('breakfast', 'lunch', 'dinner', 'snack')");
                    table.ForeignKey(
                        name: "FK_Recipes_Cuisines_CuisineId",
                        column: x => x.CuisineId,
                        principalTable: "Cuisines",
                        principalColumn: "CuisineId");
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    IngredientId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IngredientName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IngredientTypeId = table.Column<int>(type: "int", nullable: false),
                    CaloriesPer100g = table.Column<double>(type: "float", nullable: true),
                    DefaultUnit = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GramPerUnit = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.IngredientId);
                    table.ForeignKey(
                        name: "FK_Ingredients_IngredientTypes_IngredientTypeId",
                        column: x => x.IngredientTypeId,
                        principalTable: "IngredientTypes",
                        principalColumn: "IngredientTypeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NotificationUsers",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    IsRead = table.Column<bool>(type: "bit", nullable: false),
                    ReceivedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationUsers", x => new { x.UserId, x.NotificationId });
                    table.ForeignKey(
                        name: "FK_NotificationUsers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationUsers_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notifications",
                        principalColumn: "NotificationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserProfiles",
                columns: table => new
                {
                    UPId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<double>(type: "float", nullable: true),
                    GoalWeight = table.Column<double>(type: "float", nullable: true),
                    Height = table.Column<double>(type: "float", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Age = table.Column<int>(type: "int", nullable: true),
                    GoalId = table.Column<int>(type: "int", nullable: false),
                    ExperienceId = table.Column<int>(type: "int", nullable: false),
                    LevelId = table.Column<int>(type: "int", nullable: false),
                    SpeedId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserExperienceExperienceId = table.Column<int>(type: "int", nullable: false),
                    ActivityLevelLevelId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.UPId);
                    table.CheckConstraint("CK_UserProfiles_Gender", "[Gender] IN ('Male', 'Female', 'Other')");
                    table.ForeignKey(
                        name: "FK_UserProfiles_ActivityLevels_ActivityLevelLevelId",
                        column: x => x.ActivityLevelLevelId,
                        principalTable: "ActivityLevels",
                        principalColumn: "LevelId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfiles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfiles_Goals_GoalId",
                        column: x => x.GoalId,
                        principalTable: "Goals",
                        principalColumn: "GoalId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfiles_LoseWeightSpeed_SpeedId",
                        column: x => x.SpeedId,
                        principalTable: "LoseWeightSpeed",
                        principalColumn: "SpeedId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfiles_UserExperiences_UserExperienceExperienceId",
                        column: x => x.UserExperienceExperienceId,
                        principalTable: "UserExperiences",
                        principalColumn: "ExperienceId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeHealthTags",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    HealthTagId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeHealthTags", x => new { x.RecipeId, x.HealthTagId });
                    table.ForeignKey(
                        name: "FK_RecipeHealthTags_HealthTags_HealthTagId",
                        column: x => x.HealthTagId,
                        principalTable: "HealthTags",
                        principalColumn: "HealthTagId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeHealthTags_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RecipeMealTypes",
                columns: table => new
                {
                    MealId = table.Column<int>(type: "int", nullable: false),
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MealCatagorieMealId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeMealTypes", x => new { x.MealId, x.RecipeId });
                    table.ForeignKey(
                        name: "FK_RecipeMealTypes_MealCatagories_MealCatagorieMealId",
                        column: x => x.MealCatagorieMealId,
                        principalTable: "MealCatagories",
                        principalColumn: "MealId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RecipeMealTypes_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Servings",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false),
                    Ammount = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servings", x => new { x.RecipeId, x.IngredientId });
                    table.ForeignKey(
                        name: "FK_Servings_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Servings_Recipes_RecipeId",
                        column: x => x.RecipeId,
                        principalTable: "Recipes",
                        principalColumn: "RecipeId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Allergies",
                columns: table => new
                {
                    UPId = table.Column<int>(type: "int", nullable: false),
                    IngredientId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Allergies", x => new { x.IngredientId, x.UPId });
                    table.ForeignKey(
                        name: "FK_Allergies_Ingredients_IngredientId",
                        column: x => x.IngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Allergies_UserProfiles_UPId",
                        column: x => x.UPId,
                        principalTable: "UserProfiles",
                        principalColumn: "UPId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealScheduled",
                columns: table => new
                {
                    UPId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BreakfastTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LunchTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DinnerTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserProfileUPId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealScheduled", x => x.UPId);
                    table.ForeignKey(
                        name: "FK_MealScheduled_UserProfiles_UserProfileUPId",
                        column: x => x.UserProfileUPId,
                        principalTable: "UserProfiles",
                        principalColumn: "UPId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalHealthConditions",
                columns: table => new
                {
                    HealthConditionId = table.Column<int>(type: "int", nullable: false),
                    UPId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserProfileUPId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalHealthConditions", x => new { x.UPId, x.HealthConditionId });
                    table.ForeignKey(
                        name: "FK_PersonalHealthConditions_HealthConditions_HealthConditionId",
                        column: x => x.HealthConditionId,
                        principalTable: "HealthConditions",
                        principalColumn: "HealthConditionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalHealthConditions_UserProfiles_UserProfileUPId",
                        column: x => x.UserProfileUPId,
                        principalTable: "UserProfiles",
                        principalColumn: "UPId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalUserCookingSkills",
                columns: table => new
                {
                    UPId = table.Column<int>(type: "int", nullable: false),
                    CookingSkillId = table.Column<int>(type: "int", nullable: false),
                    UserProfileUPId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalUserCookingSkills", x => new { x.UPId, x.CookingSkillId });
                    table.ForeignKey(
                        name: "FK_PersonalUserCookingSkills_CookingSkills_CookingSkillId",
                        column: x => x.CookingSkillId,
                        principalTable: "CookingSkills",
                        principalColumn: "CookingSkillId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalUserCookingSkills_UserProfiles_UserProfileUPId",
                        column: x => x.UserProfileUPId,
                        principalTable: "UserProfiles",
                        principalColumn: "UPId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalUserProblem",
                columns: table => new
                {
                    UPId = table.Column<int>(type: "int", nullable: false),
                    ProblemId = table.Column<int>(type: "int", nullable: false),
                    UserProfileUPId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalUserProblem", x => new { x.UPId, x.ProblemId });
                    table.ForeignKey(
                        name: "FK_PersonalUserProblem_UserProblem_ProblemId",
                        column: x => x.ProblemId,
                        principalTable: "UserProblem",
                        principalColumn: "ProblemId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalUserProblem_UserProfiles_UserProfileUPId",
                        column: x => x.UserProfileUPId,
                        principalTable: "UserProfiles",
                        principalColumn: "UPId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubcriptionUsers",
                columns: table => new
                {
                    UPId = table.Column<int>(type: "int", nullable: false),
                    SubcriptionId = table.Column<int>(type: "int", nullable: false),
                    UserProfileUPId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubcriptionUsers", x => new { x.UPId, x.SubcriptionId });
                    table.ForeignKey(
                        name: "FK_SubcriptionUsers_Subcriptions_SubcriptionId",
                        column: x => x.SubcriptionId,
                        principalTable: "Subcriptions",
                        principalColumn: "SubcriptionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SubcriptionUsers_UserProfiles_UserProfileUPId",
                        column: x => x.UserProfileUPId,
                        principalTable: "UserProfiles",
                        principalColumn: "UPId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "1", null, "Admin", "ADMIN" },
                    { "2", null, "Staff", "STAFF" },
                    { "3", null, "Member", "MEMBER" },
                    { "4", null, "User", "USER" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Allergies_UPId",
                table: "Allergies",
                column: "UPId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Devices_UserId",
                table: "Devices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_IngredientTypeId",
                table: "Ingredients",
                column: "IngredientTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_MealScheduled_UserProfileUPId",
                table: "MealScheduled",
                column: "UserProfileUPId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationUsers_NotificationId",
                table: "NotificationUsers",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalHealthConditions_HealthConditionId",
                table: "PersonalHealthConditions",
                column: "HealthConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalHealthConditions_UserProfileUPId",
                table: "PersonalHealthConditions",
                column: "UserProfileUPId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalUserCookingSkills_CookingSkillId",
                table: "PersonalUserCookingSkills",
                column: "CookingSkillId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalUserCookingSkills_UserProfileUPId",
                table: "PersonalUserCookingSkills",
                column: "UserProfileUPId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalUserProblem_ProblemId",
                table: "PersonalUserProblem",
                column: "ProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalUserProblem_UserProfileUPId",
                table: "PersonalUserProblem",
                column: "UserProfileUPId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeHealthTags_HealthTagId",
                table: "RecipeHealthTags",
                column: "HealthTagId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeMealTypes_MealCatagorieMealId",
                table: "RecipeMealTypes",
                column: "MealCatagorieMealId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeMealTypes_RecipeId",
                table: "RecipeMealTypes",
                column: "RecipeId");

            migrationBuilder.CreateIndex(
                name: "IX_Recipes_CuisineId",
                table: "Recipes",
                column: "CuisineId");

            migrationBuilder.CreateIndex(
                name: "IX_Servings_IngredientId",
                table: "Servings",
                column: "IngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_SubcriptionUsers_SubcriptionId",
                table: "SubcriptionUsers",
                column: "SubcriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SubcriptionUsers_UserProfileUPId",
                table: "SubcriptionUsers",
                column: "UserProfileUPId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_ActivityLevelLevelId",
                table: "UserProfiles",
                column: "ActivityLevelLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_GoalId",
                table: "UserProfiles",
                column: "GoalId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_SpeedId",
                table: "UserProfiles",
                column: "SpeedId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserExperienceExperienceId",
                table: "UserProfiles",
                column: "UserExperienceExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Allergies");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Devices");

            migrationBuilder.DropTable(
                name: "MealScheduled");

            migrationBuilder.DropTable(
                name: "NotificationUsers");

            migrationBuilder.DropTable(
                name: "PersonalHealthConditions");

            migrationBuilder.DropTable(
                name: "PersonalUserCookingSkills");

            migrationBuilder.DropTable(
                name: "PersonalUserProblem");

            migrationBuilder.DropTable(
                name: "RecipeHealthTags");

            migrationBuilder.DropTable(
                name: "RecipeMealTypes");

            migrationBuilder.DropTable(
                name: "Servings");

            migrationBuilder.DropTable(
                name: "SubcriptionUsers");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "HealthConditions");

            migrationBuilder.DropTable(
                name: "CookingSkills");

            migrationBuilder.DropTable(
                name: "UserProblem");

            migrationBuilder.DropTable(
                name: "HealthTags");

            migrationBuilder.DropTable(
                name: "MealCatagories");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "Subcriptions");

            migrationBuilder.DropTable(
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "IngredientTypes");

            migrationBuilder.DropTable(
                name: "Cuisines");

            migrationBuilder.DropTable(
                name: "ActivityLevels");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "LoseWeightSpeed");

            migrationBuilder.DropTable(
                name: "UserExperiences");
        }
    }
}
