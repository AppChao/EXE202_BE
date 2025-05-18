using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EXE202_BE.Data.Models;

public partial class NotificationUsers
{
    [Key]
    [Column(Order = 1)]
    [ForeignKey("Notifications")]
    public int NotificationId { get; set; }

    [Key]
    [Column(Order = 2)]
    public string UserId { get; set; } = string.Empty;

    [ForeignKey("UserId")] 
    public virtual IdentityUser User { get; set; } = null!;

    public bool IsRead { get; set; } = false;

    public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;

    public string? Status { get; set; } = string.Empty;

    public virtual Notifications Notification { get; set; } =  null!;
}