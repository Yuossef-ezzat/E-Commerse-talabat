using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstractionLayer;
using Shared.DTOs.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Controller
{

    public class OrderController(IServiceManager _serviceManager) : ApiControllerBase
    {
        [HttpPost]
        public async Task<ActionResult<OrderToReturnDto>> CreateOrder(OrderDto orderDto)
        {
            var order = await _serviceManager.OrderService.CreateOrderAsync(orderDto, GetEmailFromToken());
            return Ok(order);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrders()
        {
            var orders = await _serviceManager.OrderService.GetAllOrderAsync(GetEmailFromToken()); 
            return Ok(orders);
        }
        [HttpGet("{Id:Guid}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderById(Guid Id)
        {
            var order = await _serviceManager.OrderService.GetOrderByIdAsync(Id); 
            return Ok(order);
        }

        //Get all DelieveryMethod
        [AllowAnonymous]
        [HttpGet("DelieveryMethods")]
        public async Task<ActionResult<IEnumerable<DelieveryMethodDto>>> GetDelieveryMethod()
        {
            var DelieveryMethod = await  _serviceManager.OrderService.GetAllDelieveryMethodAsync();
            return Ok(DelieveryMethod);
        }

    }
}
