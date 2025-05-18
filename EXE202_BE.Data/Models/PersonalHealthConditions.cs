using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class PersonalHealthConditions
{
    [ForeignKey("HealthConditions")]
    public int HealthConditionId { get; set; }

    [ForeignKey("UserProfiles")]
    public int UPId { get; set; }

    public string? Status { get; set; } = string.Empty;

    public virtual HealthConditions HealthCondition { get; set; } = null!;

    public virtual UserProfiles UserProfile { get; set; } = null!;
}