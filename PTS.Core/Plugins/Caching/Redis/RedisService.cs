using System;
using PTS.Core.Extenstions;
using System.Threading.Tasks;
using PTS.Core.Plugins.Caching.Options;
using System.Linq;

namespace PTS.Core.Plugins.Caching.Redis
{
    public class RedisService : IRedisService
    {
        private readonly RedisOptions _options;
        private readonly RedisServerService _redisServerService;
        public RedisService(RedisOptions options, RedisServerService redisServerService)
        {
            _options = options;
            _redisServerService = redisServerService;
            _redisServerService.Connect();
        }

        public string InstanceName => _options.InstanceName;
        public bool IsEnabled => _options.Enabled;

        public async Task<bool> Any(string key, int db = 0)
        {
            var dataBase = _redisServerService.GetDb(db);
            var result = await dataBase.StringGetAsync(key);
            return result.HasValue;
        }

        public async Task<T> Get<T>(string key, int db = 0) where T : new()
        {
            var dataBase = _redisServerService.GetDb(db);
            var result = await dataBase.StringGetAsync(key);
            return !result.HasValue ? new T() : result.ToString().FromJson<T>();
        }

        public async Task<string> Get(string key, int db = 0)
        {
            var dataBase = _redisServerService.GetDb(db);
            var result = await dataBase.StringGetAsync(key);
            return !result.HasValue ? "" : result.ToString();
        }

        public async Task Set(string key, string data, int? minute = null, int db = 0)
        {
            minute ??= _options.AbsoluteExpiration;
            var dataBase = _redisServerService.GetDb(db);
            await Remove(key, db);
            await dataBase.StringSetAsync(key, data, TimeSpan.FromMinutes((int)minute));
        }

        public async Task Remove(string key, int db = 0)
        {
            var dataBase = _redisServerService.GetDb(db);
            if (await Any(key, db))
            {
                await dataBase.KeyDeleteAsync(key);
            }
        }

        public async Task RemoveByPattern(string pattern, int db = 0)
        {
            var redisServer = _redisServerService.GetServer();
            var dataBase = _redisServerService.GetDb(db);
            var keys = redisServer.Keys(pattern: $"{pattern}*", database: db).ToList();
            foreach (var key in keys)
            {
                await dataBase.KeyDeleteAsync(key);
            }
            await dataBase.KeyDeleteAsync(pattern);
        }
    }
}
