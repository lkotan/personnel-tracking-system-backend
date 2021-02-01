using Microsoft.Extensions.DependencyInjection;
using PTS.Core.Plugins.Authentication;
using PTS.Core.Plugins.Authentication.Jwt;
using PTS.Core.Plugins.Caching;
using PTS.Core.Plugins.Caching.Redis;
using PTS.Core.Plugins.EmailServices;
using PTS.Core.Repositories;
using PTS.Core.Repositories.EF;
using PTS.Core.Utilities.IoC;
using System.Diagnostics;

namespace PTS.Core.Extenstions
{
    public static class ServiceInstallerExtensions
    {
        public static void InstallCoreServices(this IServiceCollection services)
        {
            services.AddSingleton<ITokenHelper, JwtHelper>();
            services.AddSingleton<IUserService, UserJwtService>();
            services.AddSingleton<ICacheService, RedisCacheService>();
            services.AddSingleton<IEmailService, EmailService>();

            services.AddSingleton<Stopwatch>();

            services.AddTransient(typeof(IRepository<>), typeof(EfRepository<,>));
            services.AddTransient(typeof(IDataAccessRepository<>), typeof(EfDataAccessRepository<>));


            ServiceTool.Create(services);
        }
    }
}
