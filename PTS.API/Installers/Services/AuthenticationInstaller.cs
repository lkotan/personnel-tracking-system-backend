using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PTS.API.Installers;
using PTS.Core.Plugins.Authentication;
using PTS.Core.Plugins.Authentication.Jwt;
using System;
using System.Text;

namespace PTS.API.Installers.Services
{
    public class AuthenticationInstaller : IInstaller
    {
        public void InstallConfigure(IApplicationBuilder app)
        {
        }

        public void InstallSerive(IServiceCollection services, IConfiguration configuration)
        {
            var jwtOptions = new JwtOptions();
            configuration.Bind(nameof(jwtOptions), jwtOptions);
            services.AddSingleton(jwtOptions);

            var loggedInUsers = new LoggedInUsers();
            configuration.Bind(nameof(loggedInUsers), loggedInUsers);
            services.AddSingleton(loggedInUsers);


            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidIssuer = jwtOptions.Issuer,
                        ValidAudience = jwtOptions.Audience,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecurityKey)),
                        ClockSkew = TimeSpan.Zero
                    };
                });
        }
    }
}
