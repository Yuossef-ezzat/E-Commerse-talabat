using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TalabatDemo.Models;

namespace TalabatDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase // Base URL/api/product
    {
        [HttpGet("{Id}")]
        public ActionResult<Product> GetProduct(int Id) // Get ::  Base URL/api/product
        {
            return new Product() { Id = Id };
        }
        [HttpGet]
        public ActionResult<Product> GetAll() // Get ::  Base URL/api/product
        {
            return new Product() { Id = 120 };
        }
        [HttpPost]
        public ActionResult<Product> AddProduct(Product product) // Post ::  Base URL/api/product
        {
            //if(ModelState.IsValid) we are not need to it because [ApiController] make it automatic 
            return product;
        }
        [HttpPost("brand")]
        public ActionResult<Product> AddBrand(Product product) // Post ::  Base URL/api/product
        {
            return product;
        }
    }
}
