using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class RecipeHealthTags
{
    public int RecipeId { get; set; }
    public int HealthTagId { get; set; }

    public string? Status { get; set; } = string.Empty;

    [ForeignKey("RecipeId")]
    public virtual Recipes Recipe { get; set; } = null!;

    [ForeignKey("HealthTagId")]
    public virtual HealthTags HealthTag { get; set; } = null!;
}