using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class HealthTags
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int HealthTagId { get; set; }

    public string? HealthTagName { get; set; } = string.Empty;
    
    public string? Description { get; set; } = string.Empty;

    public virtual ICollection<RecipeHealthTags>? RecipeHealthTags { get; set; } =  null!;

    public virtual ICollection<HealthTagConditions> HealthTagConditions { get; set; } =   null!;
}