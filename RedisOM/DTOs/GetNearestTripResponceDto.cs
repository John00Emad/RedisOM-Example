using Redis.OM.Modeling;

namespace RedisOM.DTOs;

public record GetNearestTripResponceDto(string TripId, GeoLoc Location);