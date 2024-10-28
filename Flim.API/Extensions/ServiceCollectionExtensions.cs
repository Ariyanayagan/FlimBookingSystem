using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace Flim.API.Extensions
{
    /// <summary>
    /// Extention method for swagger configuration for JWT Authorization.
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        internal static IServiceCollection AddSwaggerGenWithAuth(this IServiceCollection services)
        {
            services.AddSwaggerGen(o =>
            {
                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "JWT Authentication",
                    Description = "Enter your JWT Token here",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = JwtBearerDefaults.AuthenticationScheme,
                    BearerFormat = "JWT",
                };

                o.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, securityScheme);

                var securityRequirements = new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = JwtBearerDefaults.AuthenticationScheme
                            }
                        },
                        new List<string>()
                    }
                };

                o.AddSecurityRequirement(securityRequirements);
            });

            return services;
        }
    }
}
