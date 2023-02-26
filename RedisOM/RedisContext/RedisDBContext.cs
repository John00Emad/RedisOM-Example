using Redis.OM;
using Redis.OM.Searching;
using RedisOM.Models;

namespace RedisOM.RedisContext
{
    public class RedisDBContext
    {
        private readonly RedisConnectionProvider Provider;

        private IRedisCollection<Notification> Notifications { get; set; }
        public RedisDBContext(RedisConnectionProvider provider)
        {
            Provider = provider;
            Notifications = provider.RedisCollection<Notification>();
        }

        public async Task<string> AddNotification(Notification notification)
        {
            return await Notifications.InsertAsync(notification);
        }

        internal async Task<List<Notification>> FilterNotificationsByVehicleId(string vehicleID)
        {
            return (List<Notification>)await Notifications.Where(notification => notification.VehicleId == vehicleID).ToListAsync();
        }

        internal async Task<List<Notification>> FilterNotificationsByDismissedBy(string dismissedBy)
        {
            return (List<Notification>)await Notifications.Where(notification => notification.DismissedBy == dismissedBy).ToListAsync();
        }

        public async Task<List<Notification>> GetAllNotifications()
        {
            return (List<Notification>)await Notifications.ToListAsync();
        }
    }
}
