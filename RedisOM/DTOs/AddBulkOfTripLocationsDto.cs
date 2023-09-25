namespace RedisOM.DTOs;

public record AddBulkOfTripLocationsDto(int Count, double Latitude, double Longitude, double Radius);