using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class HealthConditions
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int HealthConditionId { get; set; }

    public string? HealthConditionType { get; set; } = string.Empty;
    
    public string? HealthConditionName { get; set; } = string.Empty;
    
    public string? 	BriefDescription { get; set; } = string.Empty;

    public virtual ICollection<PersonalHealthConditions> PersonalHealthConditions { get; set; } =  null!;
}