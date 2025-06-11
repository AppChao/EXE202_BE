using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

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
                    LevelId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LevelName = table.Column<string>(type: "text", nullable: true),
                    LevelDescription = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActivityLevels", x => x.LevelId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CookingSkills",
                columns: table => new
                {
                    CookingSkillId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CookingSkillName = table.Column<string>(type: "text", nullable: true),
                    DifficultyValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CookingSkills", x => x.CookingSkillId);
                });

            migrationBuilder.CreateTable(
                name: "Cuisines",
                columns: table => new
                {
                    CuisineId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Nation = table.Column<string>(type: "text", nullable: true),
                    Region = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cuisines", x => x.CuisineId);
                });

            migrationBuilder.CreateTable(
                name: "Goals",
                columns: table => new
                {
                    GoalId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    GoalName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Goals", x => x.GoalId);
                });

            migrationBuilder.CreateTable(
                name: "HealthConditions",
                columns: table => new
                {
                    HealthConditionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HealthConditionType = table.Column<string>(type: "text", nullable: true),
                    HealthConditionName = table.Column<string>(type: "text", nullable: true),
                    BriefDescription = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthConditions", x => x.HealthConditionId);
                    table.CheckConstraint("CK_HealthConditions_Types", "\"HealthConditionType\" IN ('Metabolic & Endocrine Disorders', 'Cardiovascular & Blood Pressure Disorders', 'Digestive & Absorption Disorders', 'Liver & Kidney Disorders', 'Immune & Allergy Disorders', 'Others – Skin, Bones, Mental Health')");
                });

            migrationBuilder.CreateTable(
                name: "HealthTags",
                columns: table => new
                {
                    HealthTagId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    HealthTagName = table.Column<string>(type: "text", nullable: true),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthTags", x => x.HealthTagId);
                });

            migrationBuilder.CreateTable(
                name: "IngredientTypes",
                columns: table => new
                {
                    IngredientTypeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TypeName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientTypes", x => x.IngredientTypeId);
                });

            migrationBuilder.CreateTable(
                name: "LoseWeightSpeed",
                columns: table => new
                {
                    SpeedId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SpeedName = table.Column<string>(type: "text", nullable: true),
                    TimeToReachGoal = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoseWeightSpeed", x => x.SpeedId);
                });

            migrationBuilder.CreateTable(
                name: "MealCatagories",
                columns: table => new
                {
                    MealId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MealName = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealCatagories", x => x.MealId);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Title = table.Column<string>(type: "text", nullable: true),
                    Body = table.Column<string>(type: "text", nullable: true),
                    Type = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ScheduledTime = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdatedDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationId);
                });

            migrationBuilder.CreateTable(
                name: "Subcriptions",
                columns: table => new
                {
                    SubcriptionId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SubcriptionName = table.Column<string>(type: "text", nullable: true),
                    SubcriptionInfor = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subcriptions", x => x.SubcriptionId);
                });

            migrationBuilder.CreateTable(
                name: "UserExperiences",
                columns: table => new
                {
                    ExperienceId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ExperienceName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserExperiences", x => x.ExperienceId);
                });

            migrationBuilder.CreateTable(
                name: "UserProblem",
                columns: table => new
                {
                    ProblemId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ProblemName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProblem", x => x.ProblemId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
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
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
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
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
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
                    UserId = table.Column<string>(type: "text", nullable: false),
                    RoleId = table.Column<string>(type: "text", nullable: false)
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
                    UserId = table.Column<string>(type: "text", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
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
                    DeviceToken = table.Column<string>(type: "text", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    Platform = table.Column<string>(type: "text", nullable: true),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                    RecipeId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CuisineId = table.Column<int>(type: "integer", nullable: true),
                    Meals = table.Column<string>(type: "text", nullable: true),
                    RecipeSteps = table.Column<string>(type: "text", nullable: true),
                    InstructionVideoLink = table.Column<string>(type: "text", nullable: true),
                    RecipeName = table.Column<string>(type: "text", nullable: true),
                    TimeEstimation = table.Column<int>(type: "integer", nullable: false),
                    DifficultyEstimation = table.Column<double>(type: "double precision", nullable: false),
                    DefaultServing = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recipes", x => x.RecipeId);
                    table.CheckConstraint("CK_Recipes_Difficulty_Rating", "\"DifficultyEstimation\" BETWEEN 1 AND 5");
                    table.CheckConstraint("CK_Recipes_Meals", "\"Meals\" IN ('breakfast', 'lunch', 'dinner', 'snack')");
                    table.ForeignKey(
                        name: "FK_Recipes_Cuisines_CuisineId",
                        column: x => x.CuisineId,
                        principalTable: "Cuisines",
                        principalColumn: "CuisineId");
                });

            migrationBuilder.CreateTable(
                name: "HealthTagConditions",
                columns: table => new
                {
                    HealthConditionId = table.Column<int>(type: "integer", nullable: false),
                    HealthTagId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthTagConditions", x => new { x.HealthConditionId, x.HealthTagId });
                    table.ForeignKey(
                        name: "FK_HealthTagConditions_HealthConditions_HealthConditionId",
                        column: x => x.HealthConditionId,
                        principalTable: "HealthConditions",
                        principalColumn: "HealthConditionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HealthTagConditions_HealthTags_HealthTagId",
                        column: x => x.HealthTagId,
                        principalTable: "HealthTags",
                        principalColumn: "HealthTagId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ingredients",
                columns: table => new
                {
                    IngredientId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    IngredientName = table.Column<string>(type: "text", nullable: true),
                    IngredientTypeId = table.Column<int>(type: "integer", nullable: false),
                    CaloriesPer100g = table.Column<double>(type: "double precision", nullable: true),
                    DefaultUnit = table.Column<string>(type: "text", nullable: true),
                    GramPerUnit = table.Column<double>(type: "double precision", nullable: true),
                    IconLibrary = table.Column<string>(type: "text", nullable: true),
                    IconName = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingredients", x => x.IngredientId);
                    table.CheckConstraint("CK_Ingredients_DefaultUnit", "\"DefaultUnit\" IN ('gram', 'ml', 'piece', 'tbsp', 'tsp')");
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
                    NotificationId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false),
                    IsRead = table.Column<bool>(type: "boolean", nullable: false),
                    ReceivedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true)
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
                    UPId = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FullName = table.Column<string>(type: "text", nullable: true),
                    Weight = table.Column<double>(type: "double precision", nullable: true),
                    GoalWeight = table.Column<double>(type: "double precision", nullable: true),
                    Height = table.Column<double>(type: "double precision", nullable: true),
                    Gender = table.Column<string>(type: "text", nullable: true),
                    SubcriptionId = table.Column<int>(type: "integer", nullable: true),
                    Age = table.Column<int>(type: "integer", nullable: true),
                    GoalId = table.Column<int>(type: "integer", nullable: true),
                    ExperienceId = table.Column<int>(type: "integer", nullable: true),
                    LevelId = table.Column<int>(type: "integer", nullable: true),
                    SpeedId = table.Column<int>(type: "integer", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserPicture = table.Column<string>(type: "text", nullable: true),
                    Streak = table.Column<int>(type: "integer", nullable: false),
                    LastLoginDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.UPId);
                    table.CheckConstraint("CK_UserProfiles_Gender", "\"Gender\" IN ('Male', 'Female', 'Other', 'Prefer not to say')");
                    table.ForeignKey(
                        name: "FK_UserProfiles_ActivityLevels_LevelId",
                        column: x => x.LevelId,
                        principalTable: "ActivityLevels",
                        principalColumn: "LevelId");
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
                        principalColumn: "GoalId");
                    table.ForeignKey(
                        name: "FK_UserProfiles_LoseWeightSpeed_SpeedId",
                        column: x => x.SpeedId,
                        principalTable: "LoseWeightSpeed",
                        principalColumn: "SpeedId");
                    table.ForeignKey(
                        name: "FK_UserProfiles_Subcriptions_SubcriptionId",
                        column: x => x.SubcriptionId,
                        principalTable: "Subcriptions",
                        principalColumn: "SubcriptionId");
                    table.ForeignKey(
                        name: "FK_UserProfiles_UserExperiences_ExperienceId",
                        column: x => x.ExperienceId,
                        principalTable: "UserExperiences",
                        principalColumn: "ExperienceId");
                });

            migrationBuilder.CreateTable(
                name: "RecipeHealthTags",
                columns: table => new
                {
                    RecipeId = table.Column<int>(type: "integer", nullable: false),
                    HealthTagId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true)
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
                    MealId = table.Column<int>(type: "integer", nullable: false),
                    RecipeId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecipeMealTypes", x => new { x.MealId, x.RecipeId });
                    table.ForeignKey(
                        name: "FK_RecipeMealTypes_MealCatagories_MealId",
                        column: x => x.MealId,
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
                    RecipeId = table.Column<int>(type: "integer", nullable: false),
                    IngredientId = table.Column<int>(type: "integer", nullable: false),
                    Ammount = table.Column<string>(type: "text", nullable: true)
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
                    UPId = table.Column<int>(type: "integer", nullable: false),
                    IngredientId = table.Column<int>(type: "integer", nullable: false),
                    IngredientsIngredientId = table.Column<int>(type: "integer", nullable: true),
                    UserProfilesUPId = table.Column<int>(type: "integer", nullable: true)
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
                        name: "FK_Allergies_Ingredients_IngredientsIngredientId",
                        column: x => x.IngredientsIngredientId,
                        principalTable: "Ingredients",
                        principalColumn: "IngredientId");
                    table.ForeignKey(
                        name: "FK_Allergies_UserProfiles_UPId",
                        column: x => x.UPId,
                        principalTable: "UserProfiles",
                        principalColumn: "UPId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Allergies_UserProfiles_UserProfilesUPId",
                        column: x => x.UserProfilesUPId,
                        principalTable: "UserProfiles",
                        principalColumn: "UPId");
                });

            migrationBuilder.CreateTable(
                name: "MealScheduled",
                columns: table => new
                {
                    UPId = table.Column<int>(type: "integer", nullable: false),
                    BreakfastTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    LunchTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false),
                    DinnerTime = table.Column<TimeOnly>(type: "time without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealScheduled", x => x.UPId);
                    table.ForeignKey(
                        name: "FK_MealScheduled_UserProfiles_UPId",
                        column: x => x.UPId,
                        principalTable: "UserProfiles",
                        principalColumn: "UPId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PaymentTransactions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    OrderCode = table.Column<long>(type: "bigint", nullable: false),
                    Amount = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    PaymentLinkId = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<string>(type: "text", nullable: false),
                    UPId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTransactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PaymentTransactions_UserProfiles_UPId",
                        column: x => x.UPId,
                        principalTable: "UserProfiles",
                        principalColumn: "UPId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalHealthConditions",
                columns: table => new
                {
                    HealthConditionId = table.Column<int>(type: "integer", nullable: false),
                    UPId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: true),
                    ActivityLevelsLevelId = table.Column<int>(type: "integer", nullable: true),
                    HealthConditionsHealthConditionId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonalHealthConditions", x => new { x.UPId, x.HealthConditionId });
                    table.ForeignKey(
                        name: "FK_PersonalHealthConditions_ActivityLevels_ActivityLevelsLevel~",
                        column: x => x.ActivityLevelsLevelId,
                        principalTable: "ActivityLevels",
                        principalColumn: "LevelId");
                    table.ForeignKey(
                        name: "FK_PersonalHealthConditions_HealthConditions_HealthConditionId",
                        column: x => x.HealthConditionId,
                        principalTable: "HealthConditions",
                        principalColumn: "HealthConditionId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PersonalHealthConditions_HealthConditions_HealthConditionsH~",
                        column: x => x.HealthConditionsHealthConditionId,
                        principalTable: "HealthConditions",
                        principalColumn: "HealthConditionId");
                    table.ForeignKey(
                        name: "FK_PersonalHealthConditions_UserProfiles_UPId",
                        column: x => x.UPId,
                        principalTable: "UserProfiles",
                        principalColumn: "UPId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalUserCookingSkills",
                columns: table => new
                {
                    UPId = table.Column<int>(type: "integer", nullable: false),
                    CookingSkillId = table.Column<int>(type: "integer", nullable: false)
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
                        name: "FK_PersonalUserCookingSkills_UserProfiles_UPId",
                        column: x => x.UPId,
                        principalTable: "UserProfiles",
                        principalColumn: "UPId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PersonalUserProblem",
                columns: table => new
                {
                    UPId = table.Column<int>(type: "integer", nullable: false),
                    ProblemId = table.Column<int>(type: "integer", nullable: false)
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
                        name: "FK_PersonalUserProblem_UserProfiles_UPId",
                        column: x => x.UPId,
                        principalTable: "UserProfiles",
                        principalColumn: "UPId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ActivityLevels",
                columns: new[] { "LevelId", "LevelDescription", "LevelName" },
                values: new object[,]
                {
                    { 1, "Sedentary lifestyle, no exercise", "Low Activity" },
                    { 2, "Light exercise 1-3 days per week", "Medium Activity" },
                    { 3, "Intense exercise 3-5 days per week", "High Activity" },
                    { 4, "Daily exercise or physical job", "Very High Activity" },
                    { 5, "Athletes very hard physical job", "Extra Activity" }
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

            migrationBuilder.InsertData(
                table: "CookingSkills",
                columns: new[] { "CookingSkillId", "CookingSkillName", "DifficultyValue" },
                values: new object[,]
                {
                    { 1, "Novice", "1" },
                    { 2, "Basic", "2" },
                    { 3, "Intermediate", "3" },
                    { 4, "Advanced", "4" }
                });

            migrationBuilder.InsertData(
                table: "Cuisines",
                columns: new[] { "CuisineId", "Description", "Nation", "Region" },
                values: new object[,]
                {
                    { 1, "Taste profile: Light, delicate, mildly seasoned, not too sweet, emphasizing subtlety and balance.\n\nCommonly used ingredients: Shallots, garlic, fish sauce, fermented shrimp paste (mắm tôm), vinegar, rice wine vinegar (dấm bỗng).\n\nRepresentative dishes:\n\nHanoi Beef Pho (Phở bò Hà Nội)\n\nBun Thang (Bún thang)\n\nLa Vong Grilled Fish (Chả cá Lã Vọng)\n\nFried Spring Rolls (Nem rán / Chả giò)\n\nCrab Noodle Soup (Bún riêu cua)", "Viet Nam", "Northern" },
                    { 2, "Taste profile: Bold, spicy, and saltier than other regions.\n\nCommonly used ingredients: Chili, lemongrass, fermented anchovy paste (mắm ruốc), fermented fish sauce (mắm nêm), turmeric, pepper.\n\nRepresentative dishes:\n\nHue Spicy Beef Noodle Soup (Bún bò Huế)\n\nQuang-style Noodles (Mì Quảng)\n\nBaby Clam Rice (Cơm hến)\n\nSteamed Savory Rice Cakes (Bánh bèo, Bánh nậm, Bánh lọc)\n\nGrilled Pork Skewers & Fermented Pork (Nem lụi, Tré)\n\nHue is known for its royal cuisine, with dishes that are often elaborate and beautifully presented.", "Viet Nam", "Central" },
                    { 3, "Taste profile: Sweeter, richer flavors, often using coconut milk and sugar-based broths.\n\nCommonly used ingredients: Sugar, coconut milk, garlic, mild chili, aromatic herbs.\n\nRepresentative dishes:\n\nPhnom Penh-style Noodle Soup (Hủ tiếu Nam Vang)\n\nFermented Fish Noodle Soup (Bún mắm)\n\nBroken Rice with Grilled Pork (Cơm tấm)\n\nFresh Spring Rolls (Gỏi cuốn)\n\nCaramelized Braised Fish in Clay Pot (Cá kho tộ)\n\nSour Soup & Fermented Fish Hotpot (Canh chua, Lẩu mắm)", "Viet Nam", "Southern" }
                });

            migrationBuilder.InsertData(
                table: "Goals",
                columns: new[] { "GoalId", "GoalName" },
                values: new object[,]
                {
                    { 1, "Eat healthy" },
                    { 2, "Learn how to cook" },
                    { 3, "Lose weight" },
                    { 4, "Gain weight" },
                    { 5, "Try new recipes" },
                    { 6, "Stick to my diet" },
                    { 7, "Build muscle" },
                    { 8, "Save time" }
                });

            migrationBuilder.InsertData(
                table: "HealthConditions",
                columns: new[] { "HealthConditionId", "BriefDescription", "HealthConditionName", "HealthConditionType" },
                values: new object[,]
                {
                    { 1, "Caused by insulin disorders – requires control of sugar and carbs", "Diabetes Type 1", "Metabolic & Endocrine Disorders" },
                    { 2, "Caused by insulin disorders – requires control of sugar and carbs", "Diabetes Type 2", "Metabolic & Endocrine Disorders" },
                    { 3, "A warning stage before diabetes", "Prediabetes", "Metabolic & Endocrine Disorders" },
                    { 4, "High LDL, triglycerides; low HDL", "Dyslipidemia", "Metabolic & Endocrine Disorders" },
                    { 5, "High uric acid causing joint inflammation – avoid purines", "Hyperuricemia / Gout", "Metabolic & Endocrine Disorders" },
                    { 6, "Excess calories – needs low-energy diet", "Overweight / Obesity", "Metabolic & Endocrine Disorders" },
                    { 7, "Includes abdominal obesity, high blood pressure, high blood sugar, high blood lipids", "Metabolic Syndrome", "Metabolic & Endocrine Disorders" },
                    { 8, "Low blood sugar – requires proper carb distribution", "Hypoglycemia", "Metabolic & Endocrine Disorders" },
                    { 9, "High blood pressure – reduce salt and fat", "Hypertension", "Cardiovascular & Blood Pressure Disorders" },
                    { 10, "Narrowed heart vessels due to fat – requires low-fat diet", "Coronary Heart Disease", "Cardiovascular & Blood Pressure Disorders" },
                    { 11, "Reduced heart pumping function – limit salt and fluids", "Heart Failure", "Cardiovascular & Blood Pressure Disorders" },
                    { 12, "Caused by cholesterol buildup", "Atherosclerosis", "Cardiovascular & Blood Pressure Disorders" },
                    { 13, "May be related to potassium, sodium, and magnesium levels", "Arrhythmia", "Cardiovascular & Blood Pressure Disorders" },
                    { 14, "Avoid acidic, spicy, and fatty foods", "Gastroesophageal Reflux Disease (GERD)", "Digestive & Absorption Disorders" },
                    { 15, "Avoid spicy, sour foods, and alcohol", "Gastric and Duodenal Ulcers", "Digestive & Absorption Disorders" },
                    { 16, "Requires low-FODMAP diet", "Irritable Bowel Syndrome (IBS)", "Digestive & Absorption Disorders" },
                    { 17, "Caused by low fiber or water intake", "Chronic Constipation", "Digestive & Absorption Disorders" },
                    { 18, "May be due to bacteria or incorrect diet", "Prolonged Diarrhea", "Digestive & Absorption Disorders" },
                    { 19, "Gluten allergy – requires gluten-free diet", "Celiac Disease / Gluten Allergy", "Digestive & Absorption Disorders" },
                    { 20, "Milk sugar allergy – avoid dairy products", "Lactose Intolerance", "Digestive & Absorption Disorders" },
                    { 21, "Caused by excess fat – needs low-fat, low-sugar diet", "Fatty Liver", "Liver & Kidney Disorders" },
                    { 22, "Weak liver – reduce protein and salt", "Hepatitis B/C, Cirrhosis", "Liver & Kidney Disorders" },
                    { 23, "Restrict protein, sodium, potassium, and phosphorus", "Chronic Kidney Disease", "Liver & Kidney Disorders" },
                    { 24, "Avoid oxalates, purines, and sodium depending on stone type", "Kidney Stones", "Liver & Kidney Disorders" },
                    { 25, "Allergic to peanuts, eggs, milk, seafood, etc.", "Food Allergies", "Immune & Allergy Disorders" },
                    { 26, "Triggered by certain foods", "Asthma related to food allergies", "Immune & Allergy Disorders" },
                    { 27, "Avoid fats and inflammatory foods", "Lupus", "Immune & Allergy Disorders" },
                    { 28, "Requires nutrient-rich, immune-boosting foods", "Immunodeficiency / Post-surgery / Cancer", "Immune & Allergy Disorders" },
                    { 29, "Calcium and vitamin D deficiency", "Osteoporosis", "Others – Skin, Bones, Mental Health" },
                    { 30, "May be linked to zinc, vitamins A and E deficiency", "Skin Inflammation, Acne", "Others – Skin, Bones, Mental Health" },
                    { 31, "Need foods rich in tryptophan, B6, and magnesium", "Mild Depression, Chronic Stress", "Others – Skin, Bones, Mental Health" },
                    { 32, "May benefit from melatonin-boosting diet", "Insomnia", "Others – Skin, Bones, Mental Health" },
                    { 33, "Requires iron-rich and vitamin C-rich foods", "Iron-deficiency Anemia", "Others – Skin, Bones, Mental Health" }
                });

            migrationBuilder.InsertData(
                table: "HealthTags",
                columns: new[] { "HealthTagId", "Description", "HealthTagName" },
                values: new object[,]
                {
                    { 1, "Low in carbohydrates, Applicable To People on weight loss diets, diabetics", "is_low_carb" },
                    { 2, "Low in fat, Applicable To People with heart conditions, those losing weight", "is_low_fat" },
                    { 3, "High in protein, Applicable To Gym-goers, people with protein deficiency", "is_high_protein" },
                    { 4, "Low in sugar, Applicable To Diabetics, people on sugar-restricted diets", "is_low_sugar" },
                    { 5, "Low in sodium, Applicable To People with high blood pressure, heart conditions", "is_low_sodium" },
                    { 6, "Low in cholesterol, Applicable To People with high cholesterol, heart conditions", "is_low_cholesterol" },
                    { 7, "Low in total calories, Applicable To Overweight individuals, people aiming for weight loss", "is_low_calorie" },
                    { 8, "High in fiber, Applicable To People with constipation, those reducing cholesterol", "is_high_fiber" },
                    { 9, "Suitable for diabetics, Applicable To People with diabetes or prediabetes", "is_diabetic_friendly" },
                    { 10, "Heart-friendly, Applicable To People with heart disease, high blood pressure", "is_heart_healthy" },
                    { 11, "Kidney-friendly, Applicable To People with chronic kidney disease or on dialysis", "is_kidney_friendly" },
                    { 12, "Liver-friendly, Applicable To People with hepatitis, fatty liver", "is_liver_friendly" },
                    { 13, "Does not trigger uric acid, Applicable To People with gout", "is_gout_safe" },
                    { 14, "Does not cause acid reflux, Applicable To People with GERD", "is_gerd_safe" },
                    { 15, "Easy to digest, Applicable To People with weak stomach, IBS", "is_digestive_friendly" },
                    { 16, "Relieves constipation, Applicable To People with constipation", "is_constipation_relief" },
                    { 17, "Helps regulate blood pressure, Applicable To People with hypertension", "is_blood_pressure_friendly" },
                    { 18, "Helps control blood lipids, Applicable To People with dyslipidemia", "is_cholesterol_control" },
                    { 19, "Gluten-free, Applicable To People with gluten allergy or celiac disease", "is_gluten_free" },
                    { 20, "Free from dairy and lactose, Applicable To People with lactose intolerance", "is_dairy_free" },
                    { 21, "Free from lactose, Applicable To People experiencing bloating or diarrhea from lactose", "is_lactose_free" },
                    { 22, "Free from common allergens, Applicable To People with multiple food allergies", "is_allergen_free" },
                    { 23, "Reduces inflammation, Applicable To People with chronic inflammatory conditions", "is_anti_inflammatory" },
                    { 24, "Keto-compliant (low-carb, high-fat), Applicable To People on keto or low-carb diets", "is_keto" },
                    { 25, "Paleo-compliant (no refined foods), Applicable To People who prefer natural, clean eating", "is_paleo" },
                    { 26, "Low FODMAP, Applicable To People with IBS or sensitive digestion", "is_fodmap_friendly" },
                    { 27, "Boosts immune system, Applicable To People recovering from illness or surgery", "is_immune_boosting" },
                    { 28, "Rich in calcium/vitamin D, Applicable To Elderly individuals, those with osteoporosis", "is_bone_strengthening" },
                    { 29, "Good for skin (vitamin E, A, zinc, etc.), Applicable To People with acne or dry skin", "is_skin_health" },
                    { 30, "Rich in lutein, vitamin A, Applicable To Screen workers, elderly individuals", "is_eye_health" },
                    { 31, "Helps improve mood, Applicable To People under stress or with mild depression", "is_mood_boosting" }
                });

            migrationBuilder.InsertData(
                table: "IngredientTypes",
                columns: new[] { "IngredientTypeId", "TypeName" },
                values: new object[,]
                {
                    { 1, "Meat & Seafood" },
                    { 2, "Vegetables" },
                    { 3, "Mushrooms" },
                    { 4, "Eggs & Dairy" },
                    { 5, "Fruits" },
                    { 6, "Carbs & Grains" },
                    { 7, "Legumes" },
                    { 8, "Spices" },
                    { 9, "Flavorings" },
                    { 10, "Fats & Oils" },
                    { 11, "Sweeteners" },
                    { 12, "Fermented Ingredients" },
                    { 13, "Others" }
                });

            migrationBuilder.InsertData(
                table: "MealCatagories",
                columns: new[] { "MealId", "Description", "MealName" },
                values: new object[,]
                {
                    { 1, "no meat or fish, but may consume eggs and dairy", "is_vegetarian" },
                    { 2, "does not consume any animal products, including dairy, eggs, and honey", "is_vegan" },
                    { 3, "suitable for Muslims: no pork, no alcohol, and meat must be slaughtered according to Islamic law", "is_halal" },
                    { 4, "suitable for Jews: follows Kosher rules, no mixing meat and dairy, only certified meat", "is_kosher" },
                    { 5, "Raw food (consumes only uncooked or minimally heated foods, typically below 42–48°C)", "is_raw_food" },
                    { 6, "no meat, but allows fish and seafood", "is_pescatarian" },
                    { 7, "includes dairy but not eggs", "is_lacto_vegetarian" },
                    { 8, "includes eggs but not dairy", "is_ovo_vegetarian" },
                    { 9, "includes both eggs and dairy", "is_lacto_ovo_vegetarian" }
                });

            migrationBuilder.InsertData(
                table: "UserExperiences",
                columns: new[] { "ExperienceId", "ExperienceName" },
                values: new object[,]
                {
                    { 1, "Beginner" },
                    { 2, "Intermediate" },
                    { 3, "Advanced" }
                });

            migrationBuilder.InsertData(
                table: "UserProblem",
                columns: new[] { "ProblemId", "ProblemName" },
                values: new object[,]
                {
                    { 1, "Lack of motivation" },
                    { 2, "Weight rebound" },
                    { 3, "No significant change" },
                    { 4, "I don't have enough time" },
                    { 5, "None of the above" }
                });

            migrationBuilder.InsertData(
                table: "HealthTagConditions",
                columns: new[] { "HealthConditionId", "HealthTagId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 4 },
                    { 1, 9 },
                    { 2, 1 },
                    { 2, 2 },
                    { 2, 4 },
                    { 2, 7 },
                    { 2, 9 },
                    { 3, 1 },
                    { 3, 4 },
                    { 3, 9 },
                    { 4, 2 },
                    { 4, 6 },
                    { 4, 10 },
                    { 4, 18 },
                    { 5, 2 },
                    { 5, 13 },
                    { 5, 22 },
                    { 6, 1 },
                    { 6, 2 },
                    { 6, 3 },
                    { 6, 7 },
                    { 7, 1 },
                    { 7, 2 },
                    { 7, 4 },
                    { 7, 5 },
                    { 7, 7 },
                    { 8, 1 },
                    { 8, 3 },
                    { 9, 5 },
                    { 9, 10 },
                    { 9, 17 },
                    { 10, 2 },
                    { 10, 6 },
                    { 10, 10 },
                    { 11, 5 },
                    { 11, 10 },
                    { 12, 2 },
                    { 12, 6 },
                    { 12, 18 },
                    { 13, 5 },
                    { 13, 10 },
                    { 13, 17 },
                    { 14, 14 },
                    { 14, 15 },
                    { 15, 14 },
                    { 15, 15 },
                    { 16, 15 },
                    { 16, 26 },
                    { 17, 8 },
                    { 17, 16 },
                    { 18, 15 },
                    { 19, 19 },
                    { 19, 22 },
                    { 20, 20 },
                    { 20, 21 },
                    { 20, 22 },
                    { 21, 2 },
                    { 21, 4 },
                    { 21, 12 },
                    { 22, 2 },
                    { 22, 11 },
                    { 22, 12 },
                    { 23, 5 },
                    { 23, 11 },
                    { 24, 2 },
                    { 24, 5 },
                    { 24, 13 },
                    { 25, 22 },
                    { 26, 22 },
                    { 27, 2 },
                    { 27, 23 },
                    { 28, 3 },
                    { 28, 23 },
                    { 28, 27 },
                    { 29, 28 },
                    { 30, 23 },
                    { 30, 29 },
                    { 31, 27 },
                    { 31, 31 },
                    { 32, 31 },
                    { 33, 3 },
                    { 33, 30 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Allergies_IngredientsIngredientId",
                table: "Allergies",
                column: "IngredientsIngredientId");

            migrationBuilder.CreateIndex(
                name: "IX_Allergies_UPId",
                table: "Allergies",
                column: "UPId");

            migrationBuilder.CreateIndex(
                name: "IX_Allergies_UserProfilesUPId",
                table: "Allergies",
                column: "UserProfilesUPId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

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
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Devices_UserId",
                table: "Devices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_HealthTagConditions_HealthTagId",
                table: "HealthTagConditions",
                column: "HealthTagId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingredients_IngredientTypeId",
                table: "Ingredients",
                column: "IngredientTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationUsers_NotificationId",
                table: "NotificationUsers",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_UPId",
                table: "PaymentTransactions",
                column: "UPId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalHealthConditions_ActivityLevelsLevelId",
                table: "PersonalHealthConditions",
                column: "ActivityLevelsLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalHealthConditions_HealthConditionId",
                table: "PersonalHealthConditions",
                column: "HealthConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalHealthConditions_HealthConditionsHealthConditionId",
                table: "PersonalHealthConditions",
                column: "HealthConditionsHealthConditionId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalUserCookingSkills_CookingSkillId",
                table: "PersonalUserCookingSkills",
                column: "CookingSkillId");

            migrationBuilder.CreateIndex(
                name: "IX_PersonalUserProblem_ProblemId",
                table: "PersonalUserProblem",
                column: "ProblemId");

            migrationBuilder.CreateIndex(
                name: "IX_RecipeHealthTags_HealthTagId",
                table: "RecipeHealthTags",
                column: "HealthTagId");

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
                name: "IX_UserProfiles_ExperienceId",
                table: "UserProfiles",
                column: "ExperienceId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_GoalId",
                table: "UserProfiles",
                column: "GoalId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_LevelId",
                table: "UserProfiles",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_SpeedId",
                table: "UserProfiles",
                column: "SpeedId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_SubcriptionId",
                table: "UserProfiles",
                column: "SubcriptionId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_UserId",
                table: "UserProfiles",
                column: "UserId",
                unique: true);
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
                name: "HealthTagConditions");

            migrationBuilder.DropTable(
                name: "MealScheduled");

            migrationBuilder.DropTable(
                name: "NotificationUsers");

            migrationBuilder.DropTable(
                name: "PaymentTransactions");

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
                name: "UserProfiles");

            migrationBuilder.DropTable(
                name: "HealthTags");

            migrationBuilder.DropTable(
                name: "MealCatagories");

            migrationBuilder.DropTable(
                name: "Ingredients");

            migrationBuilder.DropTable(
                name: "Recipes");

            migrationBuilder.DropTable(
                name: "ActivityLevels");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Goals");

            migrationBuilder.DropTable(
                name: "LoseWeightSpeed");

            migrationBuilder.DropTable(
                name: "Subcriptions");

            migrationBuilder.DropTable(
                name: "UserExperiences");

            migrationBuilder.DropTable(
                name: "IngredientTypes");

            migrationBuilder.DropTable(
                name: "Cuisines");
        }
    }
}
