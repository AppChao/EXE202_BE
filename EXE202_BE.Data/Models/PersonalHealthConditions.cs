using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class PersonalHealthConditions
{
    [Key]
    [Column(Order = 1)]
    [ForeignKey("HealthConditions")]
    public int HealthConditionId { get; set; }
    
    [Key]
    [Column(Order = 2)]
    [ForeignKey("UserProfiles")]
    public int UPId { get; set; }

    public string? Status { get; set; } = string.Empty;

    public virtual HealthConditions HealthCondition { get; set; } = null!;

    public virtual UserProfiles UserProfile { get; set; } = null!;
}