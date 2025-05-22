using EXE202_BE.Data.Models;

namespace EXE202_BE.Repository.Interface;

public interface IDevicesRepository : IGenericRepository<Devices>
{
    Task<bool> CreateDeviceToken(string userId, string fcmToken);
    
    Task<Devices?> GetDeviceToken();
}