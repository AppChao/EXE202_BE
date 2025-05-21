namespace EXE202_BE.Data.DTOS.Notifications;

public class NotificationsDTO
{
    public int NotificationId { get; set; }

    public string? Title { get; set; }

    public string? Body { get; set; }

    public string? Type { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? ScheduledTime { get; set; }

    public string? Status { get; set; }
}