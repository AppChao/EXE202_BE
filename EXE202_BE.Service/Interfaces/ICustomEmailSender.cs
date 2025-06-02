using Microsoft.AspNetCore.Identity;

namespace EXE202_BE.Service.Interface;

public interface ICustomEmailSender<TUser> : IEmailSender<TUser> where TUser : class
{
    Task SendSubscriptionRenewReminderAsync(TUser user, string email, DateTime? endDate);
}