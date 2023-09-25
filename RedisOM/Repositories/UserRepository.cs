using System.Globalization;
using Bogus;
using Bogus.Locations;
using Redis.OM;
using Redis.OM.Modeling;
using RedisOM.DTOs;
using RedisOM.Models;
using RedisOM.RedisContext;
using StackExchange.Redis;

namespace RedisOM.Repositories;

public class UserRepository
{
    private readonly RedisDbContext _redisDbContext;
    private readonly Faker _faker;

    public UserRepository(RedisDbContext redisDbContext, Faker faker)
    {
        _redisDbContext = redisDbContext;
        _faker = faker;
    }

    public async Task<IList<User>> GetNearestUesrs(GeoLoc location, double radius, GeoLocDistanceUnit distanceUnit)
    {
        return await _redisDbContext.GetNearestUsers(location, radius, distanceUnit);
    }

    public async Task<IList<GetNearestUsersResponseDto>> GetNearestUesrsWithNative(GeoLoc location, double radius,
        GeoLocDistanceUnit distanceUnit)
    {
        var result = await _redisDbContext.GetNearestUsersWithNative(location, radius, distanceUnit);
        return result;
    }

    public async Task<string> AddUser(string name, GeoLoc currentLocation)
    {
        var user = new User
        {
            Id = Guid.NewGuid().ToString(),
            Name = name,
            CurrentLocation = currentLocation
        };
        return await _redisDbContext.AddUser(user);
    }

    public async Task<IList<User>> GetAllUsers()
    {
        return await _redisDbContext.GetAllUser();
    }

    public async Task AddBulkOfUsers(AddBulkOfUsersDto bulkOfUsersDto)
    {
        var users = Enumerable.Range(0, bulkOfUsersDto.Count).Select(i =>
        {
            var randomLocation = _faker.Location().AreaCircle(bulkOfUsersDto.Latitude, bulkOfUsersDto.Longitude, bulkOfUsersDto.Radius);
            var user = new User();
            user.Id = Guid.NewGuid().ToString();
            user.Name = $"{_faker.Name.FirstName()}_{user.Id}";
            user.CurrentLocation = new GeoLoc(randomLocation.Longitude, randomLocation.Latitude);

            return user;
        }).ToList();

        await _redisDbContext.AddBulkUserLocations(users);
    }
    
   
}