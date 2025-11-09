using Microsoft.AspNetCore.Mvc;
using TalabatDemo.Factories;

namespace TalabatDemo.Extentions
{
    public static class ServiceRegistretions
    {
        public static IServiceCollection AddSwaggerServices(this IServiceCollection Services)
        {
            Services.AddEndpointsApiExplorer();
            Services.AddSwaggerGen();
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
    }
}
