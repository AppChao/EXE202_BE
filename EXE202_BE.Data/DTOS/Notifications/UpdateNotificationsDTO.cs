namespace EXE202_BE.Data.DTOS.Notifications;

public class UpdateNotificationsDTO
{
    public string? Title { get; set; }

    public string? Body { get; set; }

    public string? Type { get; set; }

    public string? Status { get; set; }

    public DateTime? ScheduledTime { get; set; }
}