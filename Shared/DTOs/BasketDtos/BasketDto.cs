using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DTOs.BasketDtos
{
    public class BasketDto
    {
        public string Id { get; set; }=null!;
        public List<BasketItemDto> BasketItems { get; set; }=new List<BasketItemDto>();
    }
}
