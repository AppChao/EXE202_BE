using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class UserExperiences
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ExperienceId { get; set; }

    public string? ExperienceName { get; set; } =  string.Empty;

    public virtual ICollection<UserProfiles> UserProfiles { get; set; } = null!;
}