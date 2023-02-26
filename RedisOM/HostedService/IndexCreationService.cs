using Redis.OM;
using RedisOM.Models;

namespace RedisOM.HostedService
{
    public class IndexCreationService : IHostedService
    {

        private readonly RedisConnectionProvider _provicder;

        public IndexCreationService(RedisConnectionProvider provicder)
        {
            _provicder = provicder;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            var exisitedIndices = (await _provicder.Connection.ExecuteAsync("FT._LIST")).ToArray().Select(x => x.ToString());

            // if (exisitedIndices.Any(index => index == "notification-idx"))
            // {
            //     await _provicder.Connection.DropIndexAsync(typeof(Notification));
            // }
            await _provicder.Connection.CreateIndexAsync(typeof(Notification));
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
