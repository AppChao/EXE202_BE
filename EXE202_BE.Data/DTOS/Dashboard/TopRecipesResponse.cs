namespace EXE202_BE.Data.DTOS.Dashboard;

public class TopRecipesResponse
{
    public int RecipeId { get; set; }
    public string RecipeName { get; set; }
    public int SelectionCount { get; set; }
}