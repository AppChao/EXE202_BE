using EXE202_BE.Data.Models;
using EXE202_BE.Repository.Interface;

namespace EXE202_BE.Repository.Repositories;

public class NotificationsRepository : GenericRepository<Notifications>, INotificationsRepository
{
    public NotificationsRepository(AppDbContext context) : base(context)
    {
    }
}