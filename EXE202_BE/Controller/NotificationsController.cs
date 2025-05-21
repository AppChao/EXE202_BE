using EXE202_BE.Data.DTOS.Notifications;
using EXE202_BE.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace EXE202_BE.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotificationsController : ControllerBase
{
    private readonly INotificationService _notificationService;

    public NotificationsController(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetNotifications(
        [FromQuery] string? searchTerm,
        [FromQuery] string? typeFilter,
        [FromQuery] string? sortColumn,
        [FromQuery] string? sortOrder,
        [FromQuery] int page = 1,
        [FromQuery] int pageSize = 20
    )
    {
        try
        {
            if (page < 1)
                return BadRequest(new { Message = "Page number must be greater than 0." });
            if (pageSize < 1 || pageSize > 10)
                return BadRequest(new { Message = "Page size must be between 1 and 10." });
            var notifications =
                await _notificationService.GetNotificationsAsync(searchTerm, typeFilter, sortColumn, sortOrder, page,
                    pageSize);
            return Ok(notifications);
        }
        catch (Exception ex)
        {
            return StatusCode(500,
                new { Message = "An error occurred while retrieving notifications.", Error = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetNotificationById(int id)
    {
        try
        {
            var notification = await _notificationService.GetNotificationByIdAsync(id);
            return Ok(notification);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { Message = $"Notification with ID {id} not found." });
        }
        catch (Exception ex)
        {
            return StatusCode(500,
                new { Message = "An error occurred while retrieving the notification.", Error = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateNotification([FromBody] CreateNotificationsDTO dto)
    {
        if (dto == null)
            return BadRequest(new { Message = "Invalid notification data." });

        try
        {
            var createdNotification = await _notificationService.CreateNotificationAsync(dto);
            return Ok(createdNotification);
        }
        catch (Exception ex)
        {
            return StatusCode(500,
                new { Message = "An error occurred while creating the notification.", Error = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateNotification(int id, [FromBody] UpdateNotificationsDTO dto)
    {
        if (dto == null)
            return BadRequest(new { Message = "Invalid notification data." });

        try
        {
            var updatedNotification = await _notificationService.UpdateNotificationAsync(id, dto);
            return Ok(updatedNotification);
        }
        catch (KeyNotFoundException)
        {
            return NotFound(new { Message = $"Notification with ID {id} not found." });
        }
        catch (Exception ex)
        {
            return StatusCode(500,
                new { Message = "An error occurred while updating the notification.", Error = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNotification(int id)
    {
        try
        {
            var result = await _notificationService.DeleteNotificationAsync(id);
            if (result)
                return NoContent();

            return NotFound(new { Message = $"Notification with ID {id} not found." });
        }
        catch (Exception ex)
        {
            return StatusCode(500,
                new { Message = "An error occurred while deleting the notification.", Error = ex.Message });
        }
    }
}