using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using ServiceAbstractionLayer;
using Shared.DTOs.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.Services
{
    public class BasketService (IBasketRepository _basketRepository ,IMapper _mapper ) : IBasketService
    {
        public async Task<BasketDto> CreateOrUpdateBasketAsync(BasketDto basket)
        {
            var CustomerBasket = _mapper.Map<CustomerBasket>(basket);
            var createdOrUpdated = await _basketRepository.CreateOrUpdateBasketAsync(CustomerBasket);
            if (createdOrUpdated == null)
                throw new Exception("Failed to create or update basket.");
            return await GetBasketAsync(basket.Id);
        }

        public async Task<bool> DeleteBasketAsync(string Id)
        {
            return await _basketRepository.DeleteBasketAsync(Id);
        }

        public async Task<BasketDto> GetBasketAsync(string Id)
        {
            var basket = await _basketRepository.GetBasketAsync(Id);
            if (basket is null)
                throw new BasketNotFoundException(Id);
            return _mapper.Map<BasketDto>(basket);
        }
    }
}
