using Redis.OM;
using Redis.OM.Modeling;

namespace RedisOM.DTOs;

public record GetNearestUsersDto(GeoLoc Location, double Radius, GeoLocDistanceUnit DistanceUnit);
