using AutoMapper;
using EXE202_BE.Repository.Interface;
using EXE202_BE.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EXE202_BE.Controllers;

[Route("api/[controller]")]
[ApiController]
public class GoalsController : ControllerBase
{
    private readonly IGoalsService _goalsService;

    public GoalsController(IGoalsService goalsService)
    {
        _goalsService = goalsService;
    }

    [HttpGet]
    public async Task<IActionResult> GetGoals()
    {
        try
        {
            var goals = await _goalsService.GetAllGoalsAsync();
            return Ok(goals);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "cannot fetch goals", Error = ex.Message });
        }    
    }
}