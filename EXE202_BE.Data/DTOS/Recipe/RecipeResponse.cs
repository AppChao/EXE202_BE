namespace EXE202_BE.Data.DTOS.Recipe;

public class RecipeResponse
{
    public int RecipeId { get; set; }
    public string RecipeName { get; set; }
    public int CuisineId { get; set; }
    public string CuisineName { get; set; } // Từ Cuisines.Nation + Region
    public string Meals { get; set; }
    public string RecipeSteps { get; set; }
    public string InstructionVideoLink { get; set; }
    public int TimeEstimation { get; set; }
    public int DifficultyEstimation { get; set; }
    public int DefaultServing { get; set; }
    public List<ServingResponse> Servings { get; set; } = new List<ServingResponse>();
    public List<string> HealthTags { get; set; } = new List<string>();
    public List<string> MealTypes { get; set; } = new List<string>();
}
