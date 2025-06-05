using EXE202_BE.Data.DTOS.Goals;

namespace EXE202_BE.Service.Interface;

public interface IGoalsService
{
    Task<List<GoalResponse>> GetAllGoalsAsync();
}