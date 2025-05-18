using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class PersonalUserProblem
{
    public int UPId { get; set; }
    
    public int ProblemId { get; set; }

    [ForeignKey("UPId")]
    public virtual UserProfiles UserProfile { get; set; } =  null!;

    [ForeignKey("ProblemId")]
    public virtual UserProblem UserProblem { get; set; } =  null!;
}