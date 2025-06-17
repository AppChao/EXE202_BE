using EXE202_BE.Data.DTOS.Auth;
using EXE202_BE.Data.Models;

namespace EXE202_BE.Service.Interface;

public interface IMealScheduledService
{
    Task<MealScheduled> CreateMealScheduled(int UPId,SignUpRequest model);

    Task<MealScheduled?> GetMealScheduleByUPId(int UPId);
}