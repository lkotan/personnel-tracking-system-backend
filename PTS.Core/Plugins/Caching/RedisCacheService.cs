using PTS.Core.Plugins.Caching.Redis;
using System.Threading.Tasks;

namespace PTS.Core.Plugins.Caching
{
    public class RedisCacheService : ICacheService
    {
        private readonly IRedisService _redis;
        public RedisCacheService(IRedisService redis)
        {
            _redis = redis;
        }

        public bool IsEnabled
        {
            get => _redis.IsEnabled;
            set { }
        }

        public async Task<bool> Any(string key, int db = 0)
        {
            key = $"{_redis.InstanceName}.{key}";
            return await _redis.Any(key, db);
        }

        public async Task<T> Get<T>(string key, int db = 0) where T : new()
        {
            key = $"{_redis.InstanceName}.{key}";
            return await _redis.Get<T>(key, db);
        }

        public async Task<string> Get(string key, int db = 0)
        {
            key = $"{_redis.InstanceName}.{key}";
            return await _redis.Get(key, db);
        }

        public async Task Remove(string key, int db = 0)
        {
            key = $"{_redis.InstanceName}.{key}";
            await _redis.Remove(key, db);
        }

        public async Task RemoveByPattern(string key, int db = 0)
        {
            key = $"{_redis.InstanceName}.{key}";
            await _redis.RemoveByPattern(key, db);
        }

        public async Task Set(string key, string data, int? minute = null, int db = 0)
        {
            key = key.Contains(_redis.InstanceName) ? key : $"{_redis.InstanceName}.{key}";
            await _redis.Set(key, data, minute, db);
        }
    }
}
