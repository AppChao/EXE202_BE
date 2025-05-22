using EXE202_BE.Data.DTOS;
using EXE202_BE.Data.DTOS.Notifications;
using EXE202_BE.Repository.Interface;
using EXE202_BE.Service.Interface;

namespace EXE202_BE.Service.Services;

using FirebaseAdmin.Messaging;
using EXE202Notification = EXE202_BE.Data.Models.Notifications;
using FCMNotification = FirebaseAdmin.Messaging.Notification;

public class NotificationsService : INotificationService
{
    private readonly INotificationsRepository _notificationRepository;
    private readonly IDevicesRepository _deviceTokenRepository;

    public NotificationsService(INotificationsRepository notificationRepository,
        IDevicesRepository deviceTokenRepository)
    {
        _notificationRepository = notificationRepository;
        _deviceTokenRepository = deviceTokenRepository;
    }

    private static Func<EXE202Notification, object> GetSortProperty(string SortColumn)
    {
        return SortColumn?.ToLower() switch
        {
            "content" => n => n.Body,
            "createddate" => n => n.CreatedAt,
            "updateddate" => n => n.UpdatedDate,
            _ => n => n.NotificationId
        };
    }

    public async Task<PageListResponse<NotificationsDTO>> GetNotificationsAsync(string? searchTerm, string? typeFilter,
        string? sortColumn, string? sortOrder, int page = 1,
        int pageSize = 20)
    {
        var notifications = await _notificationRepository.GetAllAsync(n => n.Status != "Deleted"
                                                                           && (typeFilter == null ||
                                                                               n.Type == typeFilter)
                                                                           && (string.IsNullOrWhiteSpace(searchTerm)
                                                                               || n.Body.Contains(searchTerm,
                                                                                   StringComparison.OrdinalIgnoreCase)
                                                                               || n.Title.Contains(searchTerm,
                                                                                   StringComparison.OrdinalIgnoreCase))
        );


        if (!string.IsNullOrWhiteSpace(sortColumn))
        {
            notifications = sortOrder?.ToLower() == "desc"
                ? notifications.OrderByDescending(GetSortProperty(sortColumn))
                : notifications.OrderBy(GetSortProperty(sortColumn));
        }

        var totalCount = notifications.Count();
        var paginatedNotifications = notifications
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToList();

        return new PageListResponse<NotificationsDTO>
        {
            Items = paginatedNotifications.Select(MapToDTO).ToList(),
            Page = page,
            PageSize = pageSize,
            TotalCount = totalCount,
            HasNextPage = (page * pageSize) < totalCount,
            HasPreviousPage = page > 1
        };
    }

    public async Task<NotificationsDTO> GetNotificationByIdAsync(int id)
    {
        var notification = await _notificationRepository.GetAsync(n => n.NotificationId == id)
                           ?? throw new Exception("Notification not found");
        return MapToDTO(notification);
    }

    public async Task<NotificationsDTO> CreateNotificationAsync(CreateNotificationsDTO dto)
    {
        var notification = new EXE202Notification
        {
            Title = dto.Title,
            Body = dto.Body,
            Type = dto.Type,
            CreatedAt = DateTime.UtcNow.AddHours(7),
            ScheduledTime = dto.ScheduledTime,
            Status = dto.ScheduledTime.HasValue ? "Pending" : "Active"
        };

        if (notification.Status == "Active" && !notification.ScheduledTime.HasValue)
        {
            notification.ScheduledTime = DateTime.UtcNow.AddHours(7);
        }

        await _notificationRepository.AddAsync(notification);

        // Send immediately if Active
        if (notification.Status == "Active")
        {
            await SendNotificationAsync(notification);
        }

        return MapToDTO(notification);
    }

    public async Task<NotificationsDTO> UpdateNotificationAsync(int id, UpdateNotificationsDTO dto)
    {
        var notification = await _notificationRepository.GetAsync(n => n.NotificationId == id)
                           ?? throw new KeyNotFoundException("Notification not found.");

        // Store the original status for comparison
        var originalStatus = notification.Status;

        notification.Title = dto.Title ?? notification.Title;
        notification.Type = dto.Type ?? notification.Type;
        notification.UpdatedDate = DateTime.UtcNow.AddHours(7);
        notification.Status = dto.Status ?? notification.Status;
        notification.ScheduledTime = dto.ScheduledTime ?? notification.ScheduledTime;

        // Handle status transition logic
        if (notification.Status == "Active")
        {
            // If status is Active and it wasn't before, send immediately
            if (originalStatus != "Active")
            {
                await SendNotificationAsync(notification);

                // Set ScheduledTime to now if it was null (for consistency with Create)
                if (!notification.ScheduledTime.HasValue)
                {
                    notification.ScheduledTime = DateTime.UtcNow.AddHours(7);
                }
            }
            // If ScheduledTime is still set but status is Active, clear it (optional, based on your logic)
            else if (notification.ScheduledTime.HasValue && dto.ScheduledTime == null)
            {
                notification.ScheduledTime = DateTime.UtcNow.AddHours(7); // Or null, depending on your preference
                await SendNotificationAsync(notification);
            }
        }
        else if (notification.Status == "Pending" && notification.ScheduledTime == null)
        {
            // If status is Pending but no ScheduledTime, it’s invalid—throw or set a default
            throw new ArgumentException("Pending notifications must have a ScheduledTime.");
        }

        var updatedNotification = await _notificationRepository.UpdateAsync(notification);
        return MapToDTO(updatedNotification);
    }

    public async Task<bool> DeleteNotificationAsync(int id)
    {
        var notification = await _notificationRepository.GetAsync(n => n.NotificationId == id);
        if (notification == null) return false;

        notification.Status = "Deleted";
        await _notificationRepository.UpdateAsync(notification);
        return true;
    }

    public async Task SendNotificationAsync(EXE202Notification notification)
    {
        var fcmTokens = await _deviceTokenRepository.GetDeviceToken();
        if (fcmTokens != null) return;
        try
        {
            // Create the message payload
            var message = new Message
            {
                Notification = new FCMNotification
                {
                    Title = notification.Title,
                    Body = notification.Body,
                },
                Token = fcmTokens.DeviceToken // Sending to multiple device tokens
            };

            // Send the notification
            var response = await FirebaseMessaging.DefaultInstance.SendAsync(message);

            Console.WriteLine($"FCM Response: {response} messages were sent successfully.");
            Console.WriteLine($"FCM Notification Sent at {DateTime.UtcNow.AddHours(7)}");

            notification.Status = "Active";
            await _notificationRepository.UpdateAsync(notification);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending notification: {ex.Message} at {DateTime.UtcNow.AddHours(7)}");
            notification.Status = "Failed";
            await _notificationRepository.UpdateAsync(notification);
        }
    }

    private static NotificationsDTO MapToDTO(EXE202Notification notification)
    {
        return new NotificationsDTO
        {
            NotificationId = notification.NotificationId,
            Title = notification.Title,
            Body = notification.Body,
            Type = notification.Type,
            CreatedAt = notification.CreatedAt,
            ScheduledTime = notification.ScheduledTime,
            Status = notification.Status
        };
    }
}