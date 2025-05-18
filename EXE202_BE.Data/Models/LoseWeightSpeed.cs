using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class LoseWeightSpeed
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int SpeedId { get; set; }

    public string? SpeedName { get; set; } =   string.Empty;

    //months
    public string? TimeToReachGoal { get; set; } =   string.Empty;
}