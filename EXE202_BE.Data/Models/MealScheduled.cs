using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class MealScheduled
{
    [Key]
    public int UPId { get; set; }

    public TimeOnly BreakfastTime { get; set; } =  TimeOnly.FromDateTime(DateTime.Now);

    public TimeOnly LunchTime { get; set; } =   TimeOnly.FromDateTime(DateTime.Now);

    public TimeOnly DinnerTime { get; set; } = TimeOnly.FromDateTime(DateTime.Now);

    [ForeignKey("UPId")]
    public virtual UserProfiles UserProfile { get; set; } = null!;
}