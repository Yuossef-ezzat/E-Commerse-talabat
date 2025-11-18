using DomainLayer.Models.OrderModels;
using Shared.DTOs;
using Shared.DTOs.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.MappingProfiles
{
    public class OrderProfile : AutoMapper.Profile
    {
        public OrderProfile() { 
            
            CreateMap<AddressDto,OrderAddress>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(dest => dest.DeliveryMethod,
                    Options => Options.MapFrom(src => src.DeliveryMethod.ShortName));

            CreateMap<OrderItem, OrderItemDto>()
                .ForMember(dest => dest.ProductName, option => option.MapFrom(src => src.Product.ProductName))
                .ForMember(dest => dest.PictureUrl, option => option.MapFrom<PictureItemResolver>());

            CreateMap<DelieveryMethodDto,DeliveryMethod>().ReverseMap(); 
                

        }
    }
}
