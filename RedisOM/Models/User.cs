using Redis.OM.Modeling;

namespace RedisOM.Models;

[Document(StorageType = StorageType.Json, Prefixes = new[] { "User" })]
public class User
{
    [RedisIdField] [Indexed] public string Id { get; set; } = string.Empty;
    [Indexed] public string Name { get; set; } = string.Empty;
    [Indexed] public GeoLoc CurrentLocation { get; set; }
    [Indexed] public bool HasACar { get; set; }
    
}