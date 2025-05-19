namespace EXE202_BE.Data.DTOS.Dashboard;

public class DashboardResponse
{
    public int TotalUsers { get; set; }
    public int SubscriptionUsers { get; set; }
    public double? AverageSessionTime { get; set; } // Phút, null vì chưa tính
    public double ChurnRate { get; set; } // Phần trăm
    public double SubscriptionRatio { get; set; } // Phần trăm
    public List<HourlyAccess>? PeakAccessTimes { get; set; } // Null vì chưa tính
    public List<MonthlyRevenue> MonthlyRevenue { get; set; } = new List<MonthlyRevenue>();
    public List<TopRecipesResponse> TopRecipes { get; set; } = new List<TopRecipesResponse>();
}