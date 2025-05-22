using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class MealCatagories
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MealId { get; set; }
    
    public string MealName { get; set; }

    public virtual ICollection<RecipeMealTypes>? RecipeMealTypes { get; set; } =  null!;
}