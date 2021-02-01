using System.Threading.Tasks;

namespace PTS.Core.Plugins.Caching
{
    public interface ICacheService
    {
        bool IsEnabled { get; set; }
        Task<bool> Any(string key, int db = 0);

        Task<T> Get<T>(string key, int db = 0) where T : new();

        Task<string> Get(string key, int db = 0);
        Task Set(string key, string data, int? minute = null, int db = 0);
        Task Remove(string key, int db = 0);
        Task RemoveByPattern(string key, int db = 0);
    }
}
