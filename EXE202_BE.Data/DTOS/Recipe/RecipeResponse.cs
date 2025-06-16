using EXE202_BE.Data.Models;

namespace EXE202_BE.Data.DTOS.Recipe;

public class RecipeResponse
{
    public int RecipeId { get; set; }
    public string RecipeName { get; set; }
    public string Meals { get; set; }
    public int DifficultyEstimation { get; set; }
    public int TimeEstimation { get; set; } // Phút
    public string Region { get; set; }
    public int? CuisineId { get; set; } // Chỉ trả trong GET danh sách, POST, PUT
    public string? InstructionVideoLink { get; set; } // Chỉ trả trong GET chi tiết, POST, PUT
    public List<IngredientDetail>? Ingredients { get; set; } // Chỉ trả trong GET chi tiết
    public List<RecipeStep>? Steps { get; set; } // Chỉ trả trong GET chi tiết
    public string? RecipeSteps { get; set; } // Chỉ trả trong POST, PUT, string JSON
    public int? DefaultServing { get; set; } // Thêm DefaultServing
    
    public static Recipes FromDictionary(Dictionary<string, string> dict)
    {
        return new Recipes
        {
            RecipeId = int.Parse(dict["RecipeId"]),
            RecipeName = dict["RecipeName"],
            Meals = dict["Meals"],
            RecipeSteps = dict["RecipeSteps"],
            InstructionVideoLink = dict["InstructionVideoLink"],
            CuisineId = int.Parse(dict["CuisineId"]),
            TimeEstimation = int.Parse(dict["TimeEstimation"]),
            DifficultyEstimation = int.Parse(dict["DifficultyEstimation"]),
            DefaultServing = int.Parse(dict["DefaultServing"]),
        };
    }
    
    public static Servings FromServingsDictionary(Dictionary<string, string> dict)
    {
        return new Servings
        {
            RecipeId = int.Parse(dict["RecipeId"]),
            IngredientId = int.Parse(dict["IngredientId"]),
            Ammount = dict["Ammount"],
        };
    }
}


