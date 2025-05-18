using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class MealScheduled
{
    [Key]
    public int UPId { get; set; }

    public DateTime BreakfastTime { get; set; } =  DateTime.UtcNow;

    public DateTime LunchTime { get; set; } =   DateTime.UtcNow;

    public DateTime DinnerTime { get; set; } = DateTime.UtcNow;

    [ForeignKey("UPId")]
    public virtual UserProfiles UserProfile { get; set; } = null!;
}