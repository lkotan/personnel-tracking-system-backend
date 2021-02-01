using PTS.Core.Plugins.Caching.Options;
using StackExchange.Redis;
using System.Linq;

namespace PTS.Core.Plugins.Caching.Redis
{
    public class RedisServerService
    {
        public IDatabase Db { get; set; }
        private ConnectionMultiplexer _redis;
        private readonly RedisOptions _options;

        public RedisServerService(RedisOptions options)
        {
            _options = options;
        }

        public void Connect()
        {
            if (_redis == null)
                _redis = ConnectionMultiplexer.Connect(_options.ConnectionString);
        }
        public IDatabase GetDb(int db)
        {
            return _redis.GetDatabase(db);
        }
        public IServer GetServer()
        {
            var redisConnection = ConnectionMultiplexer.Connect(_options.ConnectionString);
            redisConnection.GetServer(redisConnection.GetEndPoints().First());
            return redisConnection.GetServer(redisConnection.GetEndPoints().First());
        }
    }
}
