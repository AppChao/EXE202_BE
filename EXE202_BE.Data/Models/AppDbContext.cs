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
        
        builder.Entity<IngredientTypes>().HasData(
            new IngredientTypes { IngredientTypeId = 1, TypeName = "Meat & Seafood" },
            new IngredientTypes { IngredientTypeId = 2, TypeName = "Vegetables" },
            new IngredientTypes { IngredientTypeId = 3, TypeName = "Mushrooms" },
            new IngredientTypes { IngredientTypeId = 4, TypeName = "Eggs & Dairy" },
            new IngredientTypes { IngredientTypeId = 5, TypeName = "Fruits" },
            new IngredientTypes { IngredientTypeId = 6, TypeName = "Carbs & Grains" },
            new IngredientTypes { IngredientTypeId = 7, TypeName = "Legumes" },
            new IngredientTypes { IngredientTypeId = 8, TypeName = "Spices" },
            new IngredientTypes { IngredientTypeId = 9, TypeName = "Flavorings" },
            new IngredientTypes { IngredientTypeId = 10, TypeName = "Fats & Oils" },
            new IngredientTypes { IngredientTypeId = 11, TypeName = "Sweeteners" },
            new IngredientTypes { IngredientTypeId = 12, TypeName = "Fermented Ingredients" },
            new IngredientTypes { IngredientTypeId = 13, TypeName = "Others" }
        );

        builder.Entity<MealCatagories>().HasData(
        new MealCatagories{MealId = 1, MealName = "is_vegetarian", Description = "no meat or fish, but may consume eggs and dairy"},
        new MealCatagories{MealId = 2, MealName = "is_vegan", Description = "does not consume any animal products, including dairy, eggs, and honey"},
        new MealCatagories{MealId = 3, MealName = "is_halal", Description = "suitable for Muslims: no pork, no alcohol, and meat must be slaughtered according to Islamic law"},
        new MealCatagories{MealId = 4, MealName = "is_kosher", Description = "suitable for Jews: follows Kosher rules, no mixing meat and dairy, only certified meat"},
        new MealCatagories{MealId = 5, MealName = "is_raw_food", Description = "Raw food (consumes only uncooked or minimally heated foods, typically below 42–48°C)"},
        new MealCatagories{MealId = 6, MealName = "is_pescatarian", Description = "no meat, but allows fish and seafood"},
        new MealCatagories{MealId = 7, MealName = "is_lacto_vegetarian", Description = "includes dairy but not eggs"},
        new MealCatagories{MealId = 8, MealName = "is_ovo_vegetarian", Description = "includes eggs but not dairy"},
        new MealCatagories{MealId = 9, MealName = "is_lacto_ovo_vegetarian", Description = "includes both eggs and dairy"}
        );

        builder.Entity<HealthTags>().HasData(
        new HealthTags{HealthTagId = 1, HealthTagName = "is_low_carb", Description = "Low in carbohydrates, Applicable To People on weight loss diets, diabetics"},
        new HealthTags{HealthTagId = 2, HealthTagName = "is_low_fat", Description = "Low in fat, Applicable To People with heart conditions, those losing weight"},
        new HealthTags{HealthTagId = 3, HealthTagName = "is_high_protein", Description = "High in protein, Applicable To Gym-goers, people with protein deficiency"},
        new HealthTags{HealthTagId = 4, HealthTagName = "is_low_sugar", Description = "Low in sugar, Applicable To Diabetics, people on sugar-restricted diets"},
        new HealthTags{HealthTagId = 5, HealthTagName = "is_low_sodium", Description = "Low in sodium, Applicable To People with high blood pressure, heart conditions"},
        new HealthTags{HealthTagId = 6, HealthTagName = "is_low_cholesterol", Description = "Low in cholesterol, Applicable To People with high cholesterol, heart conditions"},
        new HealthTags{HealthTagId = 7, HealthTagName = "is_low_calorie", Description = "Low in total calories, Applicable To Overweight individuals, people aiming for weight loss"},
        new HealthTags{HealthTagId = 8, HealthTagName = "is_high_fiber", Description = "High in fiber, Applicable To People with constipation, those reducing cholesterol"},
        new HealthTags{HealthTagId = 9, HealthTagName = "is_diabetic_friendly", Description = "Suitable for diabetics, Applicable To People with diabetes or prediabetes"},
        new HealthTags{HealthTagId = 10, HealthTagName = "is_heart_healthy", Description = "Heart-friendly, Applicable To People with heart disease, high blood pressure"},
        new HealthTags{HealthTagId = 11, HealthTagName = "is_kidney_friendly", Description = "Kidney-friendly, Applicable To People with chronic kidney disease or on dialysis"},
        new HealthTags{HealthTagId = 12, HealthTagName = "is_liver_friendly", Description = "Liver-friendly, Applicable To People with hepatitis, fatty liver"},
        new HealthTags{HealthTagId = 13, HealthTagName = "is_gout_safe", Description = "Does not trigger uric acid, Applicable To People with gout"},
        new HealthTags{HealthTagId = 14, HealthTagName = "is_gerd_safe", Description = "Does not cause acid reflux, Applicable To People with GERD"},
        new HealthTags{HealthTagId = 15, HealthTagName = "is_digestive_friendly", Description = "Easy to digest, Applicable To People with weak stomach, IBS"},
        new HealthTags{HealthTagId = 16, HealthTagName = "is_constipation_relief", Description = "Relieves constipation, Applicable To People with constipation"},
        new HealthTags{HealthTagId = 17, HealthTagName = "is_blood_pressure_friendly", Description = "Helps regulate blood pressure, Applicable To People with hypertension"},
        new HealthTags{HealthTagId = 18, HealthTagName = "is_cholesterol_control", Description = "Helps control blood lipids, Applicable To People with dyslipidemia"},
        new HealthTags{HealthTagId = 19, HealthTagName = "is_gluten_free", Description = "Gluten-free, Applicable To People with gluten allergy or celiac disease"},
        new HealthTags{HealthTagId = 20, HealthTagName = "is_dairy_free", Description = "Free from dairy and lactose, Applicable To People with lactose intolerance"},
        new HealthTags{HealthTagId = 21, HealthTagName = "is_lactose_free", Description = "Free from lactose, Applicable To People experiencing bloating or diarrhea from lactose"},
        new HealthTags{HealthTagId = 22, HealthTagName = "is_allergen_free", Description = "Free from common allergens, Applicable To People with multiple food allergies"},
        new HealthTags{HealthTagId = 23, HealthTagName = "is_anti_inflammatory", Description = "Reduces inflammation, Applicable To People with chronic inflammatory conditions"},
        new HealthTags{HealthTagId = 24, HealthTagName = "is_keto", Description = "Keto-compliant (low-carb, high-fat), Applicable To People on keto or low-carb diets"},
        new HealthTags{HealthTagId = 25, HealthTagName = "is_paleo", Description = "Paleo-compliant (no refined foods), Applicable To People who prefer natural, clean eating"},
        new HealthTags{HealthTagId = 26, HealthTagName = "is_fodmap_friendly", Description = "Low FODMAP, Applicable To People with IBS or sensitive digestion"},
        new HealthTags{HealthTagId = 27, HealthTagName = "is_immune_boosting", Description = "Boosts immune system, Applicable To People recovering from illness or surgery"},
        new HealthTags{HealthTagId = 28, HealthTagName = "is_bone_strengthening", Description = "Rich in calcium/vitamin D, Applicable To Elderly individuals, those with osteoporosis"},
        new HealthTags{HealthTagId = 29, HealthTagName = "is_skin_health", Description = "Good for skin (vitamin E, A, zinc, etc.), Applicable To People with acne or dry skin"},
        new HealthTags{HealthTagId = 30, HealthTagName = "is_eye_health", Description = "Rich in lutein, vitamin A, Applicable To Screen workers, elderly individuals"},
        new HealthTags{HealthTagId = 31, HealthTagName = "is_mood_boosting", Description = "Helps improve mood, Applicable To People under stress or with mild depression"}
        );

        builder.Entity<HealthConditions>().HasData(
        new HealthConditions { HealthConditionId = 1, HealthConditionName = "Diabetes Type 1", HealthConditionType = "Metabolic & Endocrine Disorders", BriefDescription = "Caused by insulin disorders – requires control of sugar and carbs" },
        new HealthConditions { HealthConditionId = 2, HealthConditionName = "Diabetes Type 2", HealthConditionType = "Metabolic & Endocrine Disorders", BriefDescription = "Caused by insulin disorders – requires control of sugar and carbs" },
        new HealthConditions { HealthConditionId = 3, HealthConditionName = "Prediabetes", HealthConditionType = "Metabolic & Endocrine Disorders", BriefDescription = "A warning stage before diabetes" },
        new HealthConditions { HealthConditionId = 4, HealthConditionName = "Dyslipidemia", HealthConditionType = "Metabolic & Endocrine Disorders", BriefDescription = "High LDL, triglycerides; low HDL" },
        new HealthConditions { HealthConditionId = 5, HealthConditionName = "Hyperuricemia / Gout", HealthConditionType = "Metabolic & Endocrine Disorders", BriefDescription = "High uric acid causing joint inflammation – avoid purines" },
        new HealthConditions { HealthConditionId = 6, HealthConditionName = "Overweight / Obesity", HealthConditionType = "Metabolic & Endocrine Disorders", BriefDescription = "Excess calories – needs low-energy diet" },
        new HealthConditions { HealthConditionId = 7, HealthConditionName = "Metabolic Syndrome", HealthConditionType = "Metabolic & Endocrine Disorders", BriefDescription = "Includes abdominal obesity, high blood pressure, high blood sugar, high blood lipids" },
        new HealthConditions { HealthConditionId = 8, HealthConditionName = "Hypoglycemia", HealthConditionType = "Metabolic & Endocrine Disorders", BriefDescription = "Low blood sugar – requires proper carb distribution" },
    
        new HealthConditions { HealthConditionId = 9, HealthConditionName = "Hypertension", HealthConditionType = "Cardiovascular & Blood Pressure Disorders", BriefDescription = "High blood pressure – reduce salt and fat" },
        new HealthConditions { HealthConditionId = 10, HealthConditionName = "Coronary Heart Disease", HealthConditionType = "Cardiovascular & Blood Pressure Disorders", BriefDescription = "Narrowed heart vessels due to fat – requires low-fat diet" },
        new HealthConditions { HealthConditionId = 11, HealthConditionName = "Heart Failure", HealthConditionType = "Cardiovascular & Blood Pressure Disorders", BriefDescription = "Reduced heart pumping function – limit salt and fluids" },
        new HealthConditions { HealthConditionId = 12, HealthConditionName = "Atherosclerosis", HealthConditionType = "Cardiovascular & Blood Pressure Disorders", BriefDescription = "Caused by cholesterol buildup" },
        new HealthConditions { HealthConditionId = 13, HealthConditionName = "Arrhythmia", HealthConditionType = "Cardiovascular & Blood Pressure Disorders", BriefDescription = "May be related to potassium, sodium, and magnesium levels" },

        new HealthConditions { HealthConditionId = 14, HealthConditionName = "Gastroesophageal Reflux Disease (GERD)", HealthConditionType = "Digestive & Absorption Disorders", BriefDescription = "Avoid acidic, spicy, and fatty foods" },
        new HealthConditions { HealthConditionId = 15, HealthConditionName = "Gastric and Duodenal Ulcers", HealthConditionType = "Digestive & Absorption Disorders", BriefDescription = "Avoid spicy, sour foods, and alcohol" },
        new HealthConditions { HealthConditionId = 16, HealthConditionName = "Irritable Bowel Syndrome (IBS)", HealthConditionType = "Digestive & Absorption Disorders", BriefDescription = "Requires low-FODMAP diet" },
        new HealthConditions { HealthConditionId = 17, HealthConditionName = "Chronic Constipation", HealthConditionType = "Digestive & Absorption Disorders", BriefDescription = "Caused by low fiber or water intake" },
        new HealthConditions { HealthConditionId = 18, HealthConditionName = "Prolonged Diarrhea", HealthConditionType = "Digestive & Absorption Disorders", BriefDescription = "May be due to bacteria or incorrect diet" },
        new HealthConditions { HealthConditionId = 19, HealthConditionName = "Celiac Disease / Gluten Allergy", HealthConditionType = "Digestive & Absorption Disorders", BriefDescription = "Gluten allergy – requires gluten-free diet" },
        new HealthConditions { HealthConditionId = 20, HealthConditionName = "Lactose Intolerance", HealthConditionType = "Digestive & Absorption Disorders", BriefDescription = "Milk sugar allergy – avoid dairy products" },

        new HealthConditions { HealthConditionId = 21, HealthConditionName = "Fatty Liver", HealthConditionType = "Liver & Kidney Disorders", BriefDescription = "Caused by excess fat – needs low-fat, low-sugar diet" },
        new HealthConditions { HealthConditionId = 22, HealthConditionName = "Hepatitis B/C, Cirrhosis", HealthConditionType = "Liver & Kidney Disorders", BriefDescription = "Weak liver – reduce protein and salt" },
        new HealthConditions { HealthConditionId = 23, HealthConditionName = "Chronic Kidney Disease", HealthConditionType = "Liver & Kidney Disorders", BriefDescription = "Restrict protein, sodium, potassium, and phosphorus" },
        new HealthConditions { HealthConditionId = 24, HealthConditionName = "Kidney Stones", HealthConditionType = "Liver & Kidney Disorders", BriefDescription = "Avoid oxalates, purines, and sodium depending on stone type" },

        new HealthConditions { HealthConditionId = 25, HealthConditionName = "Food Allergies", HealthConditionType = "Immune & Allergy Disorders", BriefDescription = "Allergic to peanuts, eggs, milk, seafood, etc." },
        new HealthConditions { HealthConditionId = 26, HealthConditionName = "Asthma related to food allergies", HealthConditionType = "Immune & Allergy Disorders", BriefDescription = "Triggered by certain foods" },
        new HealthConditions { HealthConditionId = 27, HealthConditionName = "Lupus", HealthConditionType = "Immune & Allergy Disorders", BriefDescription = "Avoid fats and inflammatory foods" },
        new HealthConditions { HealthConditionId = 28, HealthConditionName = "Immunodeficiency / Post-surgery / Cancer", HealthConditionType = "Immune & Allergy Disorders", BriefDescription = "Requires nutrient-rich, immune-boosting foods" },

        new HealthConditions { HealthConditionId = 29, HealthConditionName = "Osteoporosis", HealthConditionType = "Others – Skin, Bones, Mental Health", BriefDescription = "Calcium and vitamin D deficiency" },
        new HealthConditions { HealthConditionId = 30, HealthConditionName = "Skin Inflammation, Acne", HealthConditionType = "Others – Skin, Bones, Mental Health", BriefDescription = "May be linked to zinc, vitamins A and E deficiency" },
        new HealthConditions { HealthConditionId = 31, HealthConditionName = "Mild Depression, Chronic Stress", HealthConditionType = "Others – Skin, Bones, Mental Health", BriefDescription = "Need foods rich in tryptophan, B6, and magnesium" },
        new HealthConditions { HealthConditionId = 32, HealthConditionName = "Insomnia", HealthConditionType = "Others – Skin, Bones, Mental Health", BriefDescription = "May benefit from melatonin-boosting diet" },
        new HealthConditions { HealthConditionId = 33, HealthConditionName = "Iron-deficiency Anemia", HealthConditionType = "Others – Skin, Bones, Mental Health", BriefDescription = "Requires iron-rich and vitamin C-rich foods" }

        );

builder.Entity<HealthTagConditions>().HasData(
    new HealthTagConditions { HealthConditionId = 1, HealthTagId = 4 },
    new HealthTagConditions { HealthConditionId = 1, HealthTagId = 9 },
    new HealthTagConditions { HealthConditionId = 1, HealthTagId = 1 },
    new HealthTagConditions { HealthConditionId = 2, HealthTagId = 4 },
    new HealthTagConditions { HealthConditionId = 2, HealthTagId = 9 },
    new HealthTagConditions { HealthConditionId = 2, HealthTagId = 1 },
    new HealthTagConditions { HealthConditionId = 2, HealthTagId = 2 },
    new HealthTagConditions { HealthConditionId = 2, HealthTagId = 7 },
    new HealthTagConditions { HealthConditionId = 3, HealthTagId = 9 },
    new HealthTagConditions { HealthConditionId = 3, HealthTagId = 4 },
    new HealthTagConditions { HealthConditionId = 3, HealthTagId = 1 },
    new HealthTagConditions { HealthConditionId = 4, HealthTagId = 6 },
    new HealthTagConditions { HealthConditionId = 4, HealthTagId = 18 },
    new HealthTagConditions { HealthConditionId = 4, HealthTagId = 2 },
    new HealthTagConditions { HealthConditionId = 4, HealthTagId = 10 },
    new HealthTagConditions { HealthConditionId = 5, HealthTagId = 13 },
    new HealthTagConditions { HealthConditionId = 5, HealthTagId = 2 },
    new HealthTagConditions { HealthConditionId = 5, HealthTagId = 22 },
    new HealthTagConditions { HealthConditionId = 6, HealthTagId = 7 },
    new HealthTagConditions { HealthConditionId = 6, HealthTagId = 1 },
    new HealthTagConditions { HealthConditionId = 6, HealthTagId = 2 },
    new HealthTagConditions { HealthConditionId = 6, HealthTagId = 3 },
    new HealthTagConditions { HealthConditionId = 7, HealthTagId = 1 },
    new HealthTagConditions { HealthConditionId = 7, HealthTagId = 2 },
    new HealthTagConditions { HealthConditionId = 7, HealthTagId = 5 },
    new HealthTagConditions { HealthConditionId = 7, HealthTagId = 4 },
    new HealthTagConditions { HealthConditionId = 7, HealthTagId = 7 },
    new HealthTagConditions { HealthConditionId = 8, HealthTagId = 1 },
    new HealthTagConditions { HealthConditionId = 8, HealthTagId = 3 },
    new HealthTagConditions { HealthConditionId = 9, HealthTagId = 5 },
    new HealthTagConditions { HealthConditionId = 9, HealthTagId = 17 },
    new HealthTagConditions { HealthConditionId = 9, HealthTagId = 10 },
    new HealthTagConditions { HealthConditionId = 10, HealthTagId = 2 },
    new HealthTagConditions { HealthConditionId = 10, HealthTagId = 6 },
    new HealthTagConditions { HealthConditionId = 10, HealthTagId = 10 },
    new HealthTagConditions { HealthConditionId = 11, HealthTagId = 5 },
    new HealthTagConditions { HealthConditionId = 11, HealthTagId = 10 },
    new HealthTagConditions { HealthConditionId = 12, HealthTagId = 6 },
    new HealthTagConditions { HealthConditionId = 12, HealthTagId = 18 },
    new HealthTagConditions { HealthConditionId = 12, HealthTagId = 2 },
    new HealthTagConditions { HealthConditionId = 13, HealthTagId = 5 },
    new HealthTagConditions { HealthConditionId = 13, HealthTagId = 17 },
    new HealthTagConditions { HealthConditionId = 13, HealthTagId = 10 },
    new HealthTagConditions { HealthConditionId = 14, HealthTagId = 14 },
    new HealthTagConditions { HealthConditionId = 14, HealthTagId = 15 },
    new HealthTagConditions { HealthConditionId = 15, HealthTagId = 15 },
    new HealthTagConditions { HealthConditionId = 15, HealthTagId = 14 },
    new HealthTagConditions { HealthConditionId = 16, HealthTagId = 26 },
    new HealthTagConditions { HealthConditionId = 16, HealthTagId = 15 },
    new HealthTagConditions { HealthConditionId = 17, HealthTagId = 8 },
    new HealthTagConditions { HealthConditionId = 17, HealthTagId = 16 },
    new HealthTagConditions { HealthConditionId = 18, HealthTagId = 15 },
    new HealthTagConditions { HealthConditionId = 19, HealthTagId = 19 },
    new HealthTagConditions { HealthConditionId = 19, HealthTagId = 22 },
    new HealthTagConditions { HealthConditionId = 20, HealthTagId = 20 },
    new HealthTagConditions { HealthConditionId = 20, HealthTagId = 21 },
    new HealthTagConditions { HealthConditionId = 20, HealthTagId = 22 },
    new HealthTagConditions { HealthConditionId = 21, HealthTagId = 12 },
    new HealthTagConditions { HealthConditionId = 21, HealthTagId = 2 },
    new HealthTagConditions { HealthConditionId = 21, HealthTagId = 4 },
    new HealthTagConditions { HealthConditionId = 22, HealthTagId = 12 },
    new HealthTagConditions { HealthConditionId = 22, HealthTagId = 2 },
    new HealthTagConditions { HealthConditionId = 22, HealthTagId = 11 },
    new HealthTagConditions { HealthConditionId = 23, HealthTagId = 11 },
    new HealthTagConditions { HealthConditionId = 23, HealthTagId = 5 },
    new HealthTagConditions { HealthConditionId = 24, HealthTagId = 13 },
    new HealthTagConditions { HealthConditionId = 24, HealthTagId = 5 },
    new HealthTagConditions { HealthConditionId = 24, HealthTagId = 2 },
    new HealthTagConditions { HealthConditionId = 25, HealthTagId = 22 },
    new HealthTagConditions { HealthConditionId = 26, HealthTagId = 22 },
    new HealthTagConditions { HealthConditionId = 27, HealthTagId = 23 },
    new HealthTagConditions { HealthConditionId = 27, HealthTagId = 2 },
    new HealthTagConditions { HealthConditionId = 28, HealthTagId = 27 },
    new HealthTagConditions { HealthConditionId = 28, HealthTagId = 3 },
    new HealthTagConditions { HealthConditionId = 28, HealthTagId = 23 },
    new HealthTagConditions { HealthConditionId = 29, HealthTagId = 28 },
    new HealthTagConditions { HealthConditionId = 30, HealthTagId = 29 },
    new HealthTagConditions { HealthConditionId = 30, HealthTagId = 23 },
    new HealthTagConditions { HealthConditionId = 31, HealthTagId = 31 },
    new HealthTagConditions { HealthConditionId = 31, HealthTagId = 27 },
    new HealthTagConditions { HealthConditionId = 32, HealthTagId = 31 },
    new HealthTagConditions { HealthConditionId = 33, HealthTagId = 3 },
    new HealthTagConditions { HealthConditionId = 33, HealthTagId = 30 }
);

        builder.Entity<RecipeMealTypes>()
            .HasKey(re => new { re.MealId, re.RecipeId });

        builder.Entity<HealthTagConditions>()
            .HasKey(htc => new { htc.HealthConditionId, htc.HealthTagId });

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

        builder.Entity<Ingredients>()
            .ToTable(tb => tb.HasCheckConstraint("CK_Ingredients_DefaultUnit", "\"DefaultUnit\" IN ('gram', 'ml', 'piece', 'tbsp', 'tsp')"));

        builder.Entity<HealthConditions>()
            .ToTable(tb => tb.HasCheckConstraint("CK_HealthConditions_Types", "\"HealthConditionType\" IN ('Metabolic & Endocrine Disorders', 'Cardiovascular & Blood Pressure Disorders', 'Digestive & Absorption Disorders', 'Liver & Kidney Disorders', 'Immune & Allergy Disorders', 'Others – Skin, Bones, Mental Health')"));
        
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
        
        builder.Entity<Allergies>()
            .HasOne(a => a.UserProfile)
            .WithMany() // or .WithMany(up => up.Allergies) if reverse nav exists
            .HasForeignKey(a => a.UPId)
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