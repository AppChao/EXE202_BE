namespace EXE202_BE.Data.DTOS.Recipe;

public class RecipeHomeResponse
{
    public int RecipeId { get; set; }
    public string RecipeName { get; set; } = string.Empty;
    public int TimeEstimation { get; set; }
    public double DifficultyEstimation { get; set; }
    public string MealName { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
}