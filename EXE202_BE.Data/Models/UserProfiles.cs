using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EXE202_BE.Data.Models;

public partial class UserProfiles
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int UPId { get; set; }

    public string? FullName { get; set; } = string.Empty;

    //Kg
    public double? Weight { get; set; }

    //Kg
    public double? GoalWeight { get; set; }

    //Cm
    public double? Height { get; set; }

    public string?  Gender { get; set; }

    public int? Age { get; set; }
    
    [ForeignKey("Goals")] 
    public int GoalId { get; set; }
    
    [ForeignKey("UserExperiences")] 
    public int ExperienceId { get; set; }
    
    [ForeignKey("ActivityLevels")] 
    public int LevelId { get; set; }
    
    [ForeignKey("LoseWeightSpeed")] 
    public int SpeedId { get; set; }
    
    [Required] public string UserId { get; set; } = string.Empty;
    
    [ForeignKey("UserId")] 
    public virtual ModifyIdentityUser User { get; set; } = null!;

    public virtual Goals Goal { get; set; } =  null!;
    
    public virtual UserExperiences UserExperience { get; set; } = null!;

    public virtual ActivityLevels ActivityLevel { get; set; } = null!;

    public virtual LoseWeightSpeed LoseWeightSpeed { get; set; } =  null!;
}