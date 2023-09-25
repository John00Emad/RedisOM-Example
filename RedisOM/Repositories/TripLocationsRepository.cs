using Bogus;
using Bogus.Locations;
using Redis.OM.Modeling;
using RedisOM.DTOs;
using RedisOM.Models;
using RedisOM.RedisContext;

namespace RedisOM.Repositories;

public class TripLocationsRepository
{
    private readonly Faker _faker;
    private readonly RedisDbContext _redisDbContext;

    public TripLocationsRepository(Faker faker, RedisDbContext redisDbContext)
    {
        _faker = faker;
        _redisDbContext = redisDbContext;
    }

    public async Task AddBulkOfTripLocations(AddBulkOfTripLocationsDto bulkOfTripLocations)
    {
        var tripLocations = Enumerable.Range(0, bulkOfTripLocations.Count).Select(i =>
        {
            var randomLocation = _faker.Location().AreaCircle(bulkOfTripLocations.Latitude, bulkOfTripLocations.Longitude, bulkOfTripLocations.Radius);
            var tripLocation = new TripLocation();
            tripLocation.TripId = Guid.NewGuid().ToString();
            tripLocation.Location = new GeoLoc(randomLocation.Longitude, randomLocation.Latitude);

            return tripLocation;
        }).ToList();

        await _redisDbContext.AddBulkTripLocations(tripLocations);
    }

    public async Task<IList<TripLocation>> GetNearestTrip(GetNearestTripDto getNearestTripDto)
    {
        return await _redisDbContext.GetNearestTrip(getNearestTripDto);
    }
}