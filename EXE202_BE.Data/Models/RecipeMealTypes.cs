using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class RecipeMealTypes
{
    [Key] [Column(Order = 1)] [ForeignKey("MealCatagories")]
    public int MealId { get; set; }
    
    [Key] [Column(Order = 2)] [ForeignKey("Recipes")]
    public int RecipeId { get; set; }

    public string? Status { get; set; } = string.Empty;

    public virtual Recipes Recipe { get; set; } = null!;

    public virtual MealCatagories MealCatagorie { get; set; } = null!;
}