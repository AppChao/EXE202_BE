namespace EXE202_BE.Data.DTOS.Recipe;

public class RecipeResponse
{
    public int RecipeId { get; set; }
    public string RecipeName { get; set; }
    public string Meals { get; set; }
    public int DifficultyEstimation { get; set; }
    public int TimeEstimation { get; set; } // Phút
    public string Nation { get; set; }
    public int? CuisineId { get; set; } // Chỉ trả trong GET danh sách, POST, PUT
    public string? InstructionVideoLink { get; set; } // Chỉ trả trong GET chi tiết, POST, PUT
    public List<IngredientDetail>? Ingredients { get; set; } // Chỉ trả trong GET chi tiết
    public List<RecipeStep>? Steps { get; set; } // Chỉ trả trong GET chi tiết
    public string? RecipeSteps { get; set; } // Chỉ trả trong POST, PUT, string JSON
}


