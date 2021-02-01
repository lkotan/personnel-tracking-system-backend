using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PTS.DataAccess;

namespace PTS.API.Installers.Services
{
    public class DbInstaller : IInstaller
    {
        public void InstallConfigure(IApplicationBuilder app)
        {
        }

        public void InstallSerive(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PTSContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Api"));
            });
            //services.AddTransient<SendeYazContext>();
            services.AddTransient<DbContext, PTSContext>();
        }
    }
}
