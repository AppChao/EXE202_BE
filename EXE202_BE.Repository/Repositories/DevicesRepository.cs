using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace EXE202_BE.Repository.Repositories;

public class DevicesRepository :  GenericRepository<Devices>, IDevicesRepository
{
    private readonly AppDbContext _dbContext;
    
    public DevicesRepository(AppDbContext context) : base(context)
    {
        _dbContext = context;
    }

    public async Task<bool> CreateDeviceToken(string userId, string fcmToken)
    {
        var deviceToken = new Devices
        {
            UserId = userId,
            DeviceToken = fcmToken,
        };

        await _dbContext.AddAsync(deviceToken);
        return await _dbContext.SaveChangesAsync() > 0;
    }
    
    public async Task<Devices?> GetDeviceToken()
    {
        var tokens = await _dbContext.Devices
            .Where(d => d.DeviceToken != null)
            .ToListAsync();

        // 🔐 Safe check
        if (tokens.Count == 0)
        {
            return null;
        }

        return tokens[0]; // or use First() if not empty
    }
}