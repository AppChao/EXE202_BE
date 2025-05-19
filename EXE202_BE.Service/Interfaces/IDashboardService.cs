using EXE202_BE.Data.DTOS.Dashboard;

namespace EXE202_BE.Service.Interface;

public interface IDashboardService
{
    Task<DashboardResponse> GetDashboardAsync();
}