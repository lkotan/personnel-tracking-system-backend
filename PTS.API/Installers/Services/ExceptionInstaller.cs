using Excepticon.AspNetCore;
using Excepticon.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PTS.API.Middlewares;

namespace PTS.API.Installers.Services
{
    public class ExceptionInstaller : IInstaller
    {
        public void InstallConfigure(IApplicationBuilder app)
        {
            app.UseMiddleware(typeof(ErrorHandlingMiddleware));
            app.UseExcepticon();
        }

        public void InstallSerive(IServiceCollection services, IConfiguration configuration)
        {
            services.AddExcepticon();
        }
    }
}
