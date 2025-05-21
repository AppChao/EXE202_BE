using EXE202_BE.Data.DTOS;
using EXE202_BE.Data.DTOS.Notifications;
using FirebaseAdmin.Messaging;

namespace EXE202_BE.Service.Interface;

using EXE202Notification = EXE202_BE.Data.Models.Notifications;

public interface INotificationService
{
    Task<PageListResponse<NotificationsDTO>> GetNotificationsAsync(
        string? searchTerm, string? typeFilter, string? sortColumn, string? sortOrder, int page = 1, int pageSize = 20);

    Task<NotificationsDTO> GetNotificationByIdAsync(int id);

    Task<NotificationsDTO> CreateNotificationAsync(CreateNotificationsDTO dto);

    Task<NotificationsDTO> UpdateNotificationAsync(int id, UpdateNotificationsDTO dto);

    Task<bool> DeleteNotificationAsync(int id);
    Task SendNotificationAsync(EXE202Notification notification);
}