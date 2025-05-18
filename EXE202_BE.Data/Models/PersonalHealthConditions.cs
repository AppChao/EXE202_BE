using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class PersonalHealthConditions
{
    public int HealthConditionId { get; set; }

    public int UPId { get; set; }

    public string? Status { get; set; } = string.Empty;

    [ForeignKey("HealthConditionId")]
    public virtual HealthConditions HealthCondition { get; set; } = null!;

    [ForeignKey("UPId")]
    public virtual UserProfiles UserProfile { get; set; } = null!;
}