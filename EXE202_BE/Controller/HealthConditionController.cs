using EXE202_BE.Data.DTOS;
using EXE202_BE.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EXE202_BE.Controllers;

[Route("api/[controller]")]
[ApiController]
public class HealthConditionController : ControllerBase
{
    private readonly IHealthConditionsService _healthConditionService;

    public HealthConditionController(IHealthConditionsService healthConditionService)
    {
        _healthConditionService = healthConditionService;
    }

    [HttpGet("health-condition-types")]
    public IActionResult GetHealthConditionTypes(
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        try
        {
            var types = _healthConditionService.GetHealthConditionTypesAsync(page, pageSize);
            return Ok(types);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Lấy danh sách loại tình trạng sức khỏe thất bại.", Error = ex.Message });
        }
    }

    [HttpGet("health-conditions")]
    public async Task<IActionResult> GetHealthConditionsByType(
        [FromQuery] string type,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20)
    {
        try
        {
            var conditions = await _healthConditionService.GetHealthConditionsByTypeAsync(type, page, pageSize);
            return Ok(conditions);
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = "Lấy danh sách tình trạng sức khỏe thất bại.", Error = ex.Message });
        }
    }
}