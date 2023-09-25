using Redis.OM;
using Redis.OM.Modeling;

namespace RedisOM.DTOs;

public record GetNearestUsersResponseDto(string User, GeoLoc Location, double Distance);
