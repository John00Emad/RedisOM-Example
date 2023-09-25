using Bogus;
using Bogus.Locations;
using Redis.OM;
using Redis.OM.Modeling;
using Redis.OM.Searching;
using RedisOM.DTOs;
using RedisOM.Models;
using StackExchange.Redis;

namespace RedisOM.RedisContext
{
    public class RedisDbContext
    {
        private readonly RedisConnectionProvider _provider;

        private IRedisCollection<Notification> Notifications { get; set; }
        private IRedisCollection<User> Users { get; set; }
        private IRedisCollection<TripLocation> TripLocations { get; set; }
        private readonly IDatabaseAsync _database;
        private readonly Faker _faker;

        public RedisDbContext(RedisConnectionProvider provider, IConnectionMultiplexer connectionMultiplexer,
            Faker faker)
        {
            _provider = provider;
            _faker = faker;
            Notifications = provider.RedisCollection<Notification>();
            Users = provider.RedisCollection<User>();
            TripLocations = provider.RedisCollection<TripLocation>();
            _database = connectionMultiplexer.GetDatabase();
        }

        public async Task<string> AddNotification(Notification notification)
        {
            return await Notifications.InsertAsync(notification);
        }

        internal async Task<IList<Notification>> FilterNotificationsByVehicleId(string vehicleId)
        {
            return await Notifications.Where(notification => notification.VehicleId == vehicleId)
                .ToListAsync();
        }

        internal async Task<List<Notification>> FilterNotificationsByDismissedBy(string dismissedBy)
        {
            return (List<Notification>)await Notifications
                .Where(notification => notification.DismissedBy == dismissedBy).ToListAsync();
        }

        public async Task<List<Notification>> GetAllNotifications()
        {
            return (List<Notification>)await Notifications.ToListAsync();
        }

        public async Task<IList<User>> GetAllUser()
        {
            return await Users.ToListAsync();
        }

        public async Task<string> AddUser(User user)
        {
            await AddUserLocations(user);
            return await Users.InsertAsync(user);
        }


        public async Task<IList<User>> GetNearestUsers(GeoLoc location, double radius, GeoLocDistanceUnit distanceUnit)
        {
            var users = await Users.GeoFilter(user => user.CurrentLocation, location.Longitude,
                location.Latitude, radius, distanceUnit).ToListAsync();
            return users;
        }

        public async Task<IList<GetNearestUsersResponseDto>> GetNearestUsersWithNative(GeoLoc location, double radius,
            GeoLocDistanceUnit distanceUnit)
        {
            var users = await _database.GeoSearchAsync(key: "locations:", longitude: location.Longitude,
                latitude: location.Latitude,
                shape: new GeoSearchCircle(radius, (GeoUnit)distanceUnit), count: 1000, demandClosest: true,
                order: Order.Descending);

            var userDistanceTuple = users.Select(result => new GetNearestUsersResponseDto(result.Member.ToString(),
                    new GeoLoc(result.Position.Value.Longitude, result.Position.Value.Latitude), result.Distance.Value))
                .ToList();
            return userDistanceTuple;
        }

        public async Task AddUserLocations(User user)
        {
            RedisKey key = new($"locations:");
            GeoEntry entry = new(user.CurrentLocation.Longitude, user.CurrentLocation.Latitude, $"{user.Name}");
            await _database.GeoAddAsync(key, entry);
        }

        public async Task AddBulkUserLocations(List<User> users)
        {
            RedisKey key = new($"locations:");
            var entries = users.Select(user =>
                new GeoEntry(user.CurrentLocation.Longitude, user.CurrentLocation.Latitude, $"{user.Name}")).ToArray();
            await _database.GeoAddAsync(key, entries);
        }

        public async Task AddBulkTripLocations(List<TripLocation> users)
        {
            await TripLocations.InsertAsync(users);
        }

        public async Task<IList<TripLocation>> GetNearestTrip(GetNearestTripDto getNearestTripDto)
        {
            var tripLocations = await TripLocations.GeoFilter(loc => loc.Location, getNearestTripDto.Location.Longitude,
                getNearestTripDto.Location.Latitude, getNearestTripDto.Radius, getNearestTripDto.DistanceUnit).Take(1000).ToListAsync();//.Select(r=>new GetNearestTripResponceDto(r.TripId,r.Location)).ToListAsync();

            return tripLocations;
        }
    }
}