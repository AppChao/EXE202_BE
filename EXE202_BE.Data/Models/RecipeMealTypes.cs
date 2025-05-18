using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class RecipeMealTypes
{
    public int MealId { get; set; }
    
    public int RecipeId { get; set; }

    public string? Status { get; set; } = string.Empty;

    [ForeignKey("RecipeId")]
    public virtual Recipes Recipe { get; set; } = null!;

    [ForeignKey("MealId")]
    public virtual MealCatagories MealCatagorie { get; set; } = null!;
}