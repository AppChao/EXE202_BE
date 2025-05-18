using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class PersonalUserProblem
{
    [ForeignKey("UserProfiles")]
    public int UPId { get; set; }
    
    [ForeignKey("UserProblem")]
    public int ProblemId { get; set; }

    public virtual UserProfiles UserProfile { get; set; } =  null!;

    public virtual UserProblem UserProblem { get; set; } =  null!;
}