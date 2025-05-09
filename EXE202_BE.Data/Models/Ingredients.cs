using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class Ingredients
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IngredientId { get; set; }

    public string? IngredientName { get; set; } = string.Empty;

    [ForeignKey("IngredientTypes")]
    public int IngredientTypeId { get; set; }

    public virtual IngredientTypes IngredientType { get; set; } = null!;
}