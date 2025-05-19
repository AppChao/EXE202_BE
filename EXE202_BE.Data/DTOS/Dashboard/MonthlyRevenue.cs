namespace EXE202_BE.Data.DTOS.Dashboard;

public class MonthlyRevenue
{
    public string Month { get; set; } // Format: "YYYY-MM"
    public double Revenue { get; set; } // USD
}