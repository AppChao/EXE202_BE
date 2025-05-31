using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class HealthTagConditions
{
    public int HealthConditionId { get; set; }

    public int HealthTagId { get; set; }

    [ForeignKey("HealthConditionId")]
    public virtual HealthConditions HealthCondition { get; set; } = null!;
    
    [ForeignKey("HealthTagId")]
    public virtual HealthTags HealthTag { get; set; } = null!;
}