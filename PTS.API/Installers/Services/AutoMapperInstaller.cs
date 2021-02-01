using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PTS.API.Installers.Profiles;
using PTS.Core.Utilities.IoC;

namespace PTS.API.Installers.Services
{
    public class AutoMapperInstaller : IInstaller
    {
        public void InstallConfigure(IApplicationBuilder app)
        {
        }

        public void InstallSerive(IServiceCollection services, IConfiguration configuration)
        {
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            ServiceTool.Create(services);
        }
    }
}
