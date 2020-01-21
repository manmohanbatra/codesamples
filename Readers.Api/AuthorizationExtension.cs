using System;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using ReadersApi.Providers;

namespace ReadersApi
{
    static class AuthorizationExtension
    {
        public static IServiceCollection AddBearerAuthentication(this IServiceCollection services, IConfigManager configManager)
        {
            services.AddAuthorization(config =>
            {
                config.AddPolicy("ShouldContainRole", options => options.RequireClaim(ClaimTypes.Role));

                config.AddPolicy("ShouldBeAReader", options =>
                {
                    options.RequireAuthenticatedUser();
                    options.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    options.Requirements.Add(new ShouldBeAReaderRequirement());
                });

                config.AddPolicy("ShouldBeAnAdmin", options =>
                {
                    options.RequireAuthenticatedUser();
                    options.AuthenticationSchemes.Add(JwtBearerDefaults.AuthenticationScheme);
                    options.Requirements.Add(new ShouldBeAnAdminRequirement());
                });
            });

            services.AddAuthentication(options =>
                        {
                            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                        })
                        .AddJwtBearer(options =>
                        {
                            options.TokenValidationParameters = new TokenValidationParameters()
                            {
                                ValidateAudience = true,
                                ValidateIssuer = true,
                                ValidateLifetime = true,
                                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configManager.Key)),
                                ValidIssuer = configManager.Issuer,
                                ValidAudience = configManager.Audience
                            };

                            options.Events = new JwtBearerEvents()
                            {
                                OnAuthenticationFailed = (context) =>
                                {
                                    Console.WriteLine(context.Exception);
                                    return Task.CompletedTask;
                                },

                                OnMessageReceived = (context) =>
                                {
                                    return Task.CompletedTask;
                                },

                                OnTokenValidated = (context) =>
                                {
                                    return Task.CompletedTask;
                                }
                            };
                        });

            return services;
        }
    }
}
