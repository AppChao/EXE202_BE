using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class Ingredients
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IngredientId { get; set; }

    public string? IngredientName { get; set; } = string.Empty;
    
    public int IngredientTypeId { get; set; }

    public double? CaloriesPer100g { get; set; }

    public string? DefaultUnit { get; set; }

    public double? GramPerUnit { get; set; }

    public string? IconLibrary { get; set; } = string.Empty;

    public string? IconName { get; set; } = string.Empty;

    [ForeignKey("IngredientTypeId")]
    public virtual IngredientTypes IngredientType { get; set; } = null!;
    
    public virtual ICollection<Allergies>? Allergies { get; set; } = null!;

    public virtual ICollection<Servings>? Servings { get; set; } =  null!;
}