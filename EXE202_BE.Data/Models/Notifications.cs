using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EXE202_BE.Data.Models;

public partial class Notifications
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int NotificationId { get; set; }

    public string? Title { get; set; } =  string.Empty;

    public string? Body { get; set; } =   string.Empty;

    public string? Type { get; set; } =   string.Empty;

    public DateTime CreatedAt { get; set; } =  DateTime.UtcNow;

    public string? Status { get; set; } =  string.Empty;
}