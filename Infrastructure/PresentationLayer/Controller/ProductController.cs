using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstractionLayer;
using Shared;
using Shared.DTOs.ProductDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize]
    public class ProductsController(IServiceManager _serviceManager) : ApiControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<PaginatedResult<ProductDto>>> GetAllProducts([FromQuery]ProductQueryParams productQuery)
        {
            
            var products = await _serviceManager.ProductService.GetProductsAsync(productQuery);
            return Ok(products);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult> GetProductById(int id)
        {
            var product = await _serviceManager.ProductService.GetProductByIdAsync(id);
            if (product == null)
                return NotFound();
            return Ok(product);
        }
        [HttpGet]
        [Route("Brand")]
        public async Task<ActionResult> GetProductsByBrand()
        {
            var brands = await _serviceManager.ProductService.GetBrandsAsync();
            return Ok(brands);
        }
        [HttpGet]
        [Route("Type")]
        public async Task<ActionResult> GetProductsByType()
        {
            var types = await _serviceManager.ProductService.GetTypesAsync();
            return Ok(types);
        }
    }
    }
