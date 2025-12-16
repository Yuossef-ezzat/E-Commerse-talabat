using Shared.DTOs.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstractionLayer
{
    public interface IOrderService
    {
        Task<OrderToReturnDto> CreateOrderAsync(OrderDto OrderDto , string Email);
        Task<IEnumerable<DelieveryMethodDto>> GetAllDelieveryMethodAsync();
        Task<IEnumerable<OrderToReturnDto>> GetAllOrderAsync(string email);
        Task<OrderToReturnDto> GetOrderByIdAsync(Guid id);
    }
}
