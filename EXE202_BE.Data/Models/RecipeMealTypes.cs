using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class RecipeMealTypes
{
    [ForeignKey("MealCatagories")]
    public int MealId { get; set; }
    
    [ForeignKey("Recipes")]
    public int RecipeId { get; set; }

    public string? Status { get; set; } = string.Empty;

    public virtual Recipes Recipe { get; set; } = null!;

    public virtual MealCatagories MealCatagorie { get; set; } = null!;
}