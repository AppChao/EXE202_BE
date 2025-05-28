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

    public string? Gender { get; set; } = string.Empty;

    public int? SubcriptionId { get; set; }

    public int? Age { get; set; }

    public int? GoalId { get; set; }

    public int? ExperienceId { get; set; }

    public int? LevelId { get; set; }

    public int? SpeedId { get; set; }

    public Uri? UserPicture { get; set; } 

    [Required] public string? UserId { get; set; } = string.Empty;

    [ForeignKey("UserId")] public virtual ModifyIdentityUser User { get; set; } = null!;

    [ForeignKey("SubcriptionId")] public virtual Subcriptions Subcription { get; set; } = null!;

    [ForeignKey("GoalId")] public virtual Goals Goal { get; set; } = null!;

    [ForeignKey("ExperienceId")] public virtual UserExperiences UserExperience { get; set; } = null!;

    [ForeignKey("LevelId")] public virtual ActivityLevels ActivityLevel { get; set; } = null!;

    [ForeignKey("SpeedId")] public virtual LoseWeightSpeed LoseWeightSpeed { get; set; } = null!;

    public virtual ICollection<Allergies>? Allergies { get; set; } = new List<Allergies>();

    public virtual ICollection<PersonalHealthConditions>? PersonalHealthConditions { get; set; } =
        new List<PersonalHealthConditions>();

    public virtual ICollection<PersonalUserProblem>? PersonalUserProblems { get; set; } = null!;
   
    public virtual ICollection<PersonalUserCookingSkills>? PersonalUserCookingSkills { get; set; } =  null!;
}