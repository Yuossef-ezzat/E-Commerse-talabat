using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using TalabatDemo.Factories;

namespace TalabatDemo.Extentions
{
    public static class ServiceRegistretions
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen(options => {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    Description = "Enter 'Bearer' Followed by space And your token"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme()
                        {
                            Reference = new OpenApiReference()
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new string []{}
                    }
                });
            });
            Services.ConfigureJwtAuthentication(configuration);
            return Services;
        }
        public static IServiceCollection AddValidationServices(this IServiceCollection Services)
        {
            Services.Configure<ApiBehaviorOptions>((options) =>
            {
                options.InvalidModelStateResponseFactory = ApiResponseFactory.AddCustomApiResponceFactory;
            });
            return Services;
        }

        private static void ConfigureJwtAuthentication(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddAuthentication(Config =>
            {
                Config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(Config =>
            {
                Config.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JwtOptions:Issuer"],
                    ValidateAudience =true,
                    //ClockSkew = TimeSpan.FromHours(24),
                    ValidAudience = configuration["JwtOptions:Audience"],
                    ValidateLifetime =true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtOptions:SecretKey"]!)) ,
                };
            });
        }
    }
}
