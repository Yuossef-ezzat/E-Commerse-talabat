using DomainLayer.Models.IdentityModule;
using Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServicesLayer.MappingProfiles
{
    public class IdentityMappingProfile : AutoMapper.Profile
    {
        public IdentityMappingProfile()
        {
            CreateMap<Address, AddressDto>().ReverseMap();
        }
}
