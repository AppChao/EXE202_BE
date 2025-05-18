using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class Servings
{
    [Key]
    [Column(Order = 1)]
    [ForeignKey("Recipes")]
    public int RecipeId { get; set; }
    
    [Key]
    [Column(Order = 2)]
    [ForeignKey("Ingredients")]
    public int IngredientId { get; set; }

    public string? Ammount { get; set; } = string.Empty;

    public virtual Recipes Recipe { get; set; } =  null!;

    public virtual Ingredients Ingredient { get; set; } =   null!;
    
}