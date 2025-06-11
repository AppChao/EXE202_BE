using EXE202_BE.Data.DTOS.Auth;
using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;
using EXE202_BE.Service.Interface;

namespace EXE202_BE.Service.Services;

public class MealScheduledService : IMealScheduledService
{
    private readonly IMealScheduledRepository _mealScheduledRepository;

    public MealScheduledService(IMealScheduledRepository mealScheduledRepository)
    {
        _mealScheduledRepository = mealScheduledRepository;
    }


    public async Task<MealScheduled> CreateMealScheduled(int UPId,SignUpRequest model)
    {
        var ms = new MealScheduled
        {
            UPId = UPId,
            BreakfastTime = TimeOnly.Parse(model.mealScheduledDTO.BreakFastTime),
            LunchTime = TimeOnly.Parse(model.mealScheduledDTO.LunchTime),
            DinnerTime = TimeOnly.Parse(model.mealScheduledDTO.DinnerTime),
        };

        await _mealScheduledRepository.AddAsync(ms);
        
        return ms;
    }
}