namespace EXE202_BE.Data.DTOS.Recipe;

public class RecipeRequest
{
    public string RecipeName { get; set; } = string.Empty;
    public string Meals { get; set; } = string.Empty;
    public int DifficultyEstimation { get; set; }
    public int TimeEstimation { get; set; } = 0; // Format: "X hours Y minutes" or "X minutes"
    public int CuisineId { get; set; } // Thay Nation bằng CuisineId
    public List<IngredientDetail> Ingredients { get; set; } = new List<IngredientDetail>();
    public string InstructionVideoLink { get; set; } = string.Empty;
    public List<RecipeStep> Steps { get; set; } = new List<RecipeStep>();
}

public class IngredientDetail
{
    public string Ingredient { get; set; } = string.Empty;
    public string Amount { get; set; } = string.Empty;
    public string DefaultUnit { get; set; } = string.Empty;
}

