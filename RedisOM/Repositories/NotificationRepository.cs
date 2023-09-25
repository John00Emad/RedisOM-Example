using Redis.OM;
using RedisOM.Models;
using RedisOM.RedisContext;

namespace RedisOM.Repositories
{
    public class NotificationRepository
    {
        private readonly RedisDbContext _redisDbContext;

        public NotificationRepository(RedisDbContext redisDbContext)
        {
            _redisDbContext = redisDbContext;
        }


        public async Task<string> AddNotification(Notification notification)
        {
            return await _redisDbContext.AddNotification(notification);
        }

        public async Task<IList<Notification>> FilterNotificationsByVehicleId(string vehicleId)
        {
            return await _redisDbContext.FilterNotificationsByVehicleId(vehicleId);
        }

        internal async Task<List<Notification>> FilterNotificationsByDismissedBy(string dismissedBy)
        {
            return await _redisDbContext.FilterNotificationsByDismissedBy(dismissedBy);
        }

        public async Task<List<Notification>> GetAllNotifications()
        {
            return await _redisDbContext.GetAllNotifications();
        }
    }
}