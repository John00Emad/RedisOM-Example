using Redis.OM;
using Redis.OM.Modeling;

namespace RedisOM.DTOs;

public record GetNearestTripDto(GeoLoc Location, double Radius, GeoLocDistanceUnit DistanceUnit);