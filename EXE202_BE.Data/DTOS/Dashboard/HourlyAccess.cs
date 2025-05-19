namespace EXE202_BE.Data.DTOS.Dashboard;

public class HourlyAccess
{
    public string Hour { get; set; } // Format: "HH:00"
    public int AccessCount { get; set; }
}