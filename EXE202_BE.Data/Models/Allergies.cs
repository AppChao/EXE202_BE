using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class Allergies
{
    public int UPId { get; set; }
    
    public int IngredientId { get; set; }

    [ForeignKey("UPId")]
    public virtual UserProfiles UserProfile { get; set; } = null!;

    [ForeignKey("IngredientId")]
    public virtual Ingredients Ingredient { get; set; } = null!;
}