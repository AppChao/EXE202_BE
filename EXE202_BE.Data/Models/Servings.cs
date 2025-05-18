using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class Servings
{ 
    public int RecipeId { get; set; }
    
    public int IngredientId { get; set; }

    public string? Ammount { get; set; } = string.Empty;

    [ForeignKey("RecipeId")]
    public virtual Recipes Recipe { get; set; } =  null!;

    [ForeignKey("IngredientId")]
    public virtual Ingredients Ingredient { get; set; } =   null!;
    
}