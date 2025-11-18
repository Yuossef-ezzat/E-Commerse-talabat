using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.OrderModels;
using DomainLayer.Models.Product;
using ServiceAbstractionLayer;
using ServicesLayer.Specifications.OrderModuleSpecification;
using Shared.DTOs.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer
{
    public class OrderService(IMapper _mapper , IBasketRepository _basketRepository , IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(OrderDto OrderDto, string Email)
        {
            // Map AddressDto to OrderAddress
            var OrderAddress = _mapper.Map<OrderAddress>(OrderDto.Address);
            //Get the Basket from the BasketRepo
            var basket = await _basketRepository.GetBasketAsync(OrderDto.BasketId);
            if (basket is null)
                throw new BasketNotFoundException(OrderDto.BasketId);
            //Create Order Items
            List<OrderItem> orderItems = new List<OrderItem>();
            var _productRepo = _unitOfWork.GetRepository<Product, int>();
            foreach (var basketitem in basket.BasketItems)
            {
                var OriginalProduct = await _productRepo.GetByIdAsync(basketitem.Id)
                                    ?? throw new ProductNotFoundException(basketitem.Id);
                var OrderItem = new OrderItem()
                {
                    Product = new ProductItemOrdered()
                    {
                        ProductId = basketitem.Id,
                        ProductName = OriginalProduct.Name,
                        PictureUrl = OriginalProduct.PictureUrl,
                    },
                    Price = OriginalProduct.Price,
                    Quantity = basketitem.Quantity,
                };  

                orderItems.Add(OrderItem);
            }

            // Get Delievery Method
            var delieveryMethod = await _unitOfWork.GetRepository<DeliveryMethod, int>()
                                    .GetByIdAsync(OrderDto.DeliveryMethodId)
                                    ?? throw new DelieveryMethodNotFoundException(OrderDto.DeliveryMethodId);
            // SubTotal
            var SubTotal = orderItems.Sum(i => i.Price * i.Quantity);

            // Create Order
            var order = new Order(Email, OrderAddress , delieveryMethod, OrderDto.DeliveryMethodId, orderItems,SubTotal);
            await _unitOfWork.GetRepository<Order,Guid>().AddAsync(order);

            await _unitOfWork.SaveChangesAsync();

            // Return the Order

            return _mapper.Map<OrderToReturnDto>(order);

        }

        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrderAsync(string email)
        {
            var specs = new OrderSpecification(email);
            var Orders = await _unitOfWork.GetRepository<Order,Guid>().GetAllAsync(specs);
            return _mapper.Map<IEnumerable<OrderToReturnDto>>(Orders);
        }

        public async Task<OrderToReturnDto> GetOrderByIdAsync(Guid id)
        {
            var specs = new OrderSpecification(id);
            var Order = await _unitOfWork.GetRepository<Order, Guid>().GetByIdAsync(specs);
            return _mapper.Map<OrderToReturnDto>(Order);

        }
        public async Task<IEnumerable<DelieveryMethodDto>> GetAllDelieveryMethodAsync()
        {
            var DelieveryMethods = await _unitOfWork.GetRepository<DeliveryMethod,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DelieveryMethodDto>>(DelieveryMethods);
        }

    }
}
 