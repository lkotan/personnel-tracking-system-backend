using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace PTS.API.Installers.Services
{
    public class SwaggerInstaller : IInstaller
    {
        public void InstallConfigure(IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Personel Takip Api Service");
                c.DefaultModelsExpandDepth(-1);
                c.DocExpansion(DocExpansion.None);
            });
        }

        public void InstallSerive(IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Personel Takip Api Service",
                    Description = "Net Core 3.1",
                    Contact = new OpenApiContact
                    {
                        Name = "Lütfi KOTAN",
                        Url = new Uri("https://github.com/lkotan")

                    }
                });
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description = "JWT, Bearer Şeması. Örnek: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };
                x.AddSecurityDefinition("Bearer", securitySchema);
                var securityRequirement = new OpenApiSecurityRequirement { { securitySchema, new[] { "Bearer" } } };
                x.AddSecurityRequirement(securityRequirement);
            });
        }
    }
}
