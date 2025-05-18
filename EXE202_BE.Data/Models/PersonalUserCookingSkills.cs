using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class PersonalUserCookingSkills
{
    public int UPId { get; set; }
    
    public int CookingSkillId { get; set; }

    [ForeignKey("UPId")]
    public virtual UserProfiles UserProfile { get; set; } =  null!;

    [ForeignKey("CookingSkillId")]
    public virtual CookingSkills CookingSkill { get; set; } =   null!;
}