using AutoMapper;
using AutoMapper.Execution;
using DomainLayer.Models.OrderModels;
using Microsoft.Extensions.Configuration;
using Shared.DTOs.OrderDtos;

namespace ServicesLayer.MappingProfiles
{
    public class PictureItemResolver(IConfiguration _configuration) : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl)) return string.Empty;
            else
            {
                var pictureUrl = _configuration.GetSection("Urls")["baseUrl"];
                return $"{pictureUrl}{source.Product.PictureUrl}";
            }
        }
    }
}