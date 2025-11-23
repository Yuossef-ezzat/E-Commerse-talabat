using DomainLayer.Contracts;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstractionLayer;
using ServicesLayer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer
{
    public static class ApplicationServiceRegistertions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper((x) => { }, typeof(ServiceAssemblyReference).Assembly);
            Services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelgate>();

            Services.AddScoped<IProductService, ProductService>();
            Services.AddScoped<Func<IProductService>>(provider=>()=>provider.GetRequiredService<IProductService>());

            Services.AddScoped<IBasketService, BasketService>();
            Services.AddScoped<Func<IBasketService>>(provider => () => provider.GetRequiredService<IBasketService>());

            Services.AddScoped<IAuthenticationService, AuthenticationService>();
            Services.AddScoped<Func<IAuthenticationService>>(provider => () => provider.GetRequiredService<IAuthenticationService>());
            
            Services.AddScoped<IOrderService, OrderService>();
            Services.AddScoped<Func<IOrderService>>(provider => () => provider.GetRequiredService<IOrderService>());

            return Services;
        }
    }
}
