using AutoMapper;
using EXE202_BE.Data.DTOS.Dashboard;
using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;
using EXE202_BE.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace EXE202_BE.Service.Services;

public class DashboardService : IDashboardService
{
    private readonly IUserProfilesRepository _userProfilesRepository;
    private readonly IRecipesRepository _recipesRepository;
    private readonly UserManager<ModifyIdentityUser> _userManager;
    private readonly IMapper _mapper;

    public DashboardService(
        IUserProfilesRepository userProfilesRepository,
        IRecipesRepository recipesRepository,
        UserManager<ModifyIdentityUser> userManager,
        IMapper mapper)
    {
        _userProfilesRepository = userProfilesRepository;
        _recipesRepository = recipesRepository;
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<DashboardResponse> GetDashboardAsync()
    {
        // Lấy dữ liệu người dùng
        var userProfiles = await _userProfilesRepository.GetAllAsync();
        var users = await _userManager.Users.ToListAsync();

        // Tính toán Cards
        var totalUsers = userProfiles.Count();
        var subscriptionUsers = userProfiles.Count(up => up.SubcriptionId != 0);
        var inactiveUsers = users.Count(u => u.Status == "Inactive");
        var churnRate = totalUsers > 0 ? (double)inactiveUsers / totalUsers * 100 : 0;

        // Tính Subscription Ratio
        var subscriptionRatio = totalUsers > 0 ? (double)subscriptionUsers / totalUsers * 100 : 0;

        // Tính Monthly Revenue (giả định giá 10 USD/tháng, lấy 12 tháng gần nhất)
        var monthlyRevenue = new List<MonthlyRevenue>();
        var currentDate = DateTime.UtcNow;
        for (int i = 11; i >= 0; i--)
        {
            var month = currentDate.AddMonths(-i);
            var monthKey = month.ToString("yyyy-MM");
            // Giả định mỗi subscription có giá 10 USD
            var revenue = subscriptionUsers * 10; // Cần thay bằng logic thực tế nếu có bảng giá
            monthlyRevenue.Add(new MonthlyRevenue { Month = monthKey, Revenue = revenue });
        }

        return new DashboardResponse
        {
            TotalUsers = totalUsers,
            SubscriptionUsers = subscriptionUsers,
            AverageSessionTime = null, // Chưa có dữ liệu
            ChurnRate = churnRate,
            SubscriptionRatio = subscriptionRatio,
            PeakAccessTimes = null, // Chưa có dữ liệu
            MonthlyRevenue = monthlyRevenue,
        };
    }
}