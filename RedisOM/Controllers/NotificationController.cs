using Microsoft.AspNetCore.Mvc;
using RedisOM.Models;
using RedisOM.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace RedisOM.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        public NotificationRepository NotificationRepo { get; }

        public NotificationController(NotificationRepository notificationRepo)
        {
            NotificationRepo = notificationRepo;
        }


        [HttpGet("GetAllNotification")]
        public async Task<List<Notification>> GetAllNotifiations()
        {
            return await NotificationRepo.GetAllNotifications();
        }
        
        [HttpGet("GetByVehicleCode/{vehicleCode}")]
        public async Task<IList<Notification>> GetNotificationByVehicleCode(string vehicleCode)
        {
            return await NotificationRepo.FilterNotificationsByVehicleId(vehicleCode);
        }
        
        [HttpGet("GetByDismissedBy/{dismissedBy}")]
        public async Task<List<Notification>> GetNotificationByDismissedBy(string dismissedBy)
        {
            return await NotificationRepo.FilterNotificationsByDismissedBy(dismissedBy);
        }
        

        [HttpPost]
        public async Task<string> AddNotification([FromBody] Notification notification)
        {
            return await NotificationRepo.AddNotification(notification);
        }


    }
}
