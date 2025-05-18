using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace EXE202_BE.Data.Models;

public partial class NotificationUsers
{
    public int NotificationId { get; set; }

    public string UserId { get; set; } = string.Empty;

    [ForeignKey("UserId")] 
    public virtual ModifyIdentityUser User { get; set; } = null!;

    public bool IsRead { get; set; } = false;

    public DateTime ReceivedAt { get; set; } = DateTime.UtcNow;

    public string? Status { get; set; } = string.Empty;

    [ForeignKey("NotificationId")]
    public virtual Notifications Notification { get; set; } =  null!;
}