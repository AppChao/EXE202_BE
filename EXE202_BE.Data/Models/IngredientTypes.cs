using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class IngredientTypes
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IngredientTypeId { get; set; }

    public string? TypeName { get; set; } = string.Empty;
}