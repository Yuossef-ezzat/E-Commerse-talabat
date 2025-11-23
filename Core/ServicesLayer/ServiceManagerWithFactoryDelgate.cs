using DomainLayer.Contracts;
using ServiceAbstractionLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer
{
    public class ServiceManagerWithFactoryDelgate(Func<IProductService> _productServiceFactory,
                                                  Func<IBasketService> _basketServiceFactory,
                                                  Func<IAuthenticationService> _authenticationServiceFactory,
                                                  Func<IOrderService> _orderServiceFactory ) : IServiceManager
    {
        public IProductService ProductService => _productServiceFactory.Invoke();

        public IBasketService BasketService => _basketServiceFactory.Invoke();

        public IAuthenticationService AuthenticationService => _authenticationServiceFactory.Invoke();

        public IOrderService OrderService => _orderServiceFactory.Invoke();
    }
}
