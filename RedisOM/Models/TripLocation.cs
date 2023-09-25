using Redis.OM.Modeling;

namespace RedisOM.Models;

[Document(StorageType = StorageType.Hash, Prefixes = new[] { "TripLocations" })]
public class TripLocation
{
    [RedisIdField] [Indexed] public string Id { get; set; }
    [Indexed] public string TripId { get; set; }
    [Indexed] public GeoLoc Location { get; set; }
}   