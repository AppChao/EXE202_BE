using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class PersonalUserCookingSkills
{
    [Key]
    [ForeignKey("UserProfiles")]
    public int UPId { get; set; }
    
    [Key]
    [ForeignKey("CookingSkills")]
    public int CookingSkillId { get; set; }

    public virtual UserProfiles UserProfile { get; set; } =  null!;

    public virtual CookingSkills CookingSkill { get; set; } =   null!;
}