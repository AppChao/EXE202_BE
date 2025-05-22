using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class Goals
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int GoalId { get; set; }

    public string? GoalName { get; set; } = string.Empty;

    public virtual ICollection<UserProfiles>? UserProfiles { get; set; } =   null!;
}