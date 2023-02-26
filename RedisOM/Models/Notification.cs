using Redis.OM.Modeling;

namespace RedisOM.Models
{
    [Document(StorageType = StorageType.Json, Prefixes = new[] { "Notification" })]
    public class Notification
    {
        [RedisIdField] [Indexed] public string Id { get; set; } = string.Empty;
        [Indexed] public string Title { get; set; } = string.Empty;
        [Indexed] public string VehicleId { get; set; } = string.Empty;
        [Indexed] public string DismissedBy { get; set; } = string.Empty;
        [Indexed(Sortable = true)] public DateTime RaisedDate { get; set; }
    }
}
