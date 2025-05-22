using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class ActivityLevels
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LevelId { get; set; }

    public string? LevelName { get; set; } =  string.Empty;

    public string? LevelDescription { get; set; } =   string.Empty;

    public virtual ICollection<PersonalHealthConditions>? PersonalHealthConditions { get; set; } = null!;

    public virtual ICollection<UserProfiles>? UserProfiles { get; set; } = null!;
}