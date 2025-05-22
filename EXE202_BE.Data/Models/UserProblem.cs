using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class UserProblem
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ProblemId { get; set; }

    public string? ProblemName { get; set; } = string.Empty;

    public virtual ICollection<PersonalUserProblem>? PersonalUserProblems { get; set; }  = null!;
    
}