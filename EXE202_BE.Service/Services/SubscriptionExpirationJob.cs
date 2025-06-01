using EXE202_BE.Data.Models;
using EXE202_BE.Service.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

public class SubscriptionExpirationJob
{
    private readonly AppDbContext _dbContext;
    private readonly ILogger<SubscriptionExpirationJob> _logger;
    private readonly ICustomEmailSender<ModifyIdentityUser> _emailSender;

    public SubscriptionExpirationJob(
        AppDbContext dbContext,
        ILogger<SubscriptionExpirationJob> logger,
        ICustomEmailSender<ModifyIdentityUser> emailSender)
    {
        _dbContext = dbContext;
        _logger = logger;
        _emailSender = emailSender;
    }

    public async Task CheckAndUpdateExpiredSubscriptions()
    {
        try
        {
            var now = DateTime.UtcNow;
            var expiredProfiles = await _dbContext.UserProfiles
                .Where(up => up.EndDate != null && up.EndDate < now && up.SubcriptionId != 1)
                .ToListAsync();

            foreach (var profile in expiredProfiles)
            {
                profile.SubcriptionId = 1; // "free"
                profile.StartDate = null;
                profile.EndDate = null;
                _logger.LogInformation($"Subscription for UPId {profile.UPId} has expired. Reverted to free (SubcriptionId: 1).");
            }

            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"Checked and updated {expiredProfiles.Count} expired subscriptions.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to check and update expired subscriptions.");
        }
    }

    public async Task NotifyExpiringSubscriptions()
    {
        try
        {
            var now = DateTime.UtcNow;
            var warningPeriod = now.AddDays(3);
            var expiringProfiles = await _dbContext.UserProfiles
                .Include(up => up.User)
                .Where(up => up.EndDate != null && up.EndDate <= warningPeriod && up.EndDate > now && up.SubcriptionId != 1)
                .ToListAsync();

            foreach (var profile in expiringProfiles)
            {
                var user = profile.User;
                if (user != null && !string.IsNullOrEmpty(user.Email))
                {
                    await _emailSender.SendSubscriptionRenewReminderAsync(user, user.Email, profile.EndDate);
                }
            }

            _logger.LogInformation($"Notified {expiringProfiles.Count} users about expiring subscriptions.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Failed to notify expiring subscriptions.");
        }
    }
}