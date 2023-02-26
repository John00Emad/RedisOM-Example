using Redis.OM;
using RedisOM.Models;
using RedisOM.RedisContext;

namespace RedisOM.Repositories
{
    public class NotificationRepository
    {
        private readonly RedisDBContext RedisDBContext;

        public NotificationRepository(RedisDBContext redisDBContext)
        {
            RedisDBContext = redisDBContext;
        }


        public async Task<string> AddNotification(Notification notification)
        {
            return await RedisDBContext.AddNotification(notification);
        }

        public async Task<List<Notification>> FilterNotificationsByVehicleID(string vehicleID)
        {
            return await RedisDBContext.FilterNotificationsByVehicleId(vehicleID);
        }

        internal async Task<List<Notification>> FilterNotificationsByDismissedBy(string dismissedBy)
        {
            return await RedisDBContext.FilterNotificationsByDismissedBy(dismissedBy);
        }

        public async Task<List<Notification>> GetAllNotifications()
        {
            return await RedisDBContext.GetAllNotifications();
        }
    }
}