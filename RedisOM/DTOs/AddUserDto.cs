using Redis.OM.Modeling;

namespace RedisOM.DTOs;

public record AddUserDto(string Name, GeoLoc CurrentLocation);