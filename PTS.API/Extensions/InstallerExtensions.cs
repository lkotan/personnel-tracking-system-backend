using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PTS.API.Installers;
using System;
using System.Linq;

namespace PTS.API.Extensions
{
    public static class InstallerExtensions
    {
        public static void InstallAllService(this IServiceCollection services,IConfiguration configuration)
        {
            var installers = typeof(Startup).Assembly.ExportedTypes
                .Where(x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

            installers.ForEach(x => x.InstallSerive(services, configuration));

        }
        public static void InstallAllConfigure(this IApplicationBuilder app)
        {
            var installers = typeof(Startup).Assembly.ExportedTypes.Where(x =>
                    typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IInstaller>().ToList();
            installers.ForEach(x => x.InstallConfigure(app));
        }
    }
}
