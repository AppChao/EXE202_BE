using AutoMapper;
using EXE202_BE.Data.DTOS.Goals;
using EXE202_BE.Repository.Interface;
using EXE202_BE.Service.Interface;

namespace EXE202_BE.Service.Services;

public class GoalsService : IGoalsService
{
    private readonly IGoalsRepository _goalsRepository;
    private readonly IMapper  _mapper;

    public GoalsService(IGoalsRepository goalsRepository, IMapper mapper)
    {
        _goalsRepository = goalsRepository;
        _mapper = mapper;
    }
    
    public async Task<List<GoalResponse>> GetAllGoalsAsync()
    {
        return _mapper.Map<List<GoalResponse>>(await _goalsRepository.GetAllAsync());
    }
}