using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PTS.Core.Plugins.Caching;
using PTS.Core.Plugins.Caching.Options;
using PTS.Core.Plugins.Caching.Redis;

namespace PTS.API.Installers.Services
{
    public class RedisInstaller : IInstaller
    {
        public void InstallConfigure(IApplicationBuilder app)
        {
        }

        public void InstallSerive(IServiceCollection services, IConfiguration configuration)
        {
            var options = new RedisOptions();
            configuration.GetSection(nameof(RedisOptions)).Bind(options);
            if (options.Enabled)
            {
                services.AddSingleton(options);
                services.AddSingleton<RedisServerService>();
                services.AddSingleton<IRedisService, RedisService>();
                services.AddSingleton<ICacheService, RedisCacheService>();
            }
        }
    }
}
