using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class PersonalUserProblem
{
    [Key]
    [Column(Order = 1)]
    [ForeignKey("UserProfiles")]
    public int UPId { get; set; }
    
    [Key]
    [Column(Order = 2)]
    [ForeignKey("UserProblem")]
    public int ProblemId { get; set; }

    public virtual UserProfiles UserProfile { get; set; } =  null!;

    public virtual UserProblem UserProblem { get; set; } =  null!;
}