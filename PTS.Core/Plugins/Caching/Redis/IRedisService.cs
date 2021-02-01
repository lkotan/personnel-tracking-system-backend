using System.Threading.Tasks;

namespace PTS.Core.Plugins.Caching.Redis
{
    public interface IRedisService
    {
        string InstanceName { get; }
        bool IsEnabled { get; }
        Task<bool> Any(string key, int db = 0);
        Task<T> Get<T>(string key, int db = 0) where T : new();
        Task<string> Get(string key, int db = 0);
        Task Set(string key, string data, int? minute = null, int db = 0);
        Task Remove(string key, int db = 0);
        Task RemoveByPattern(string pattern, int db = 0);
    }
}
