using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class CookingSkills
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CookingSkillId { get; set; }

    public string? CookingSkillName { get; set; } = string.Empty;

    public string? DifficultyValue { get; set; } = string.Empty;
}