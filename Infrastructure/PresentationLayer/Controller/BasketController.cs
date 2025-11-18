using Microsoft.AspNetCore.Mvc;
using ServiceAbstractionLayer;
using Shared.DTOs.BasketDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Controller
{
    [ApiController]
    [Route("api/[Controller]")]
    public class BasketController(IServiceManager _ServiceManager) : ApiControllerBase
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<BasketDto>> GetBasket(string id)
        {
            var basket = await _ServiceManager.BasketService.GetBasketAsync(id);
            return Ok(basket);
        }
        [HttpPost]
        public async Task<ActionResult<BasketDto>> CreateOrUpdateBasket(BasketDto Basket)
        {
            var basket = await _ServiceManager.BasketService.CreateOrUpdateBasketAsync(Basket);
            return Ok(basket);
        }
        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteBasket(string id)
        {
            var result = await _ServiceManager.BasketService.DeleteBasketAsync(id);
            return Ok(result);
        }
    }
}
