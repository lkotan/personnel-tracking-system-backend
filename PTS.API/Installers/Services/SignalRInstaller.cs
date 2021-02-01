using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PTS.Business.Hubs;

namespace PTS.API.Installers.Services
{
    public class SignalRInstaller : IInstaller
    {
        public void InstallConfigure(IApplicationBuilder app)
        {
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<NotificationHub>("/NotificationHub");
                endpoints.MapHub<ChartHub>("/ChartHub");
            });
        }
        public void InstallSerive(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSignalR();
        }
    }
}
