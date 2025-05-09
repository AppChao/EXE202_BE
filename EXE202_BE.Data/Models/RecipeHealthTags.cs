using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class RecipeHealthTags
{
    [Key]
    [Column(Order = 1)]
    [ForeignKey("Recipes")]
    public int RecipeId { get; set; }
    
    [Key]
    [Column(Order = 2)]
    [ForeignKey("HealthTags")]
    public int HealthTagId { get; set; }

    public string? Status { get; set; } = string.Empty;

    public virtual Recipes Recipe { get; set; } = null!;

    public virtual HealthTags HealthTag { get; set; } = null!;
}