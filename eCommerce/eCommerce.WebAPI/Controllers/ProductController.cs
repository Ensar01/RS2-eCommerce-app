using eCommerce.Model;
using eCommerce.Model.SearchObjects;
using eCommerce.Model.Responses;
using eCommerce.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eCommerce.WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        protected readonly IProductService _productService;
        
        public ProductController(IProductService service) {
            _productService = service;
        }

        [HttpGet("")]
        public async Task<IEnumerable<ProductResponse>> Get([FromQuery]ProductSearchObject? search = null)
        {
            return await _productService.GetAsync(search ?? new ProductSearchObject());
        }

        [HttpGet("{id}")]
        public async Task<ProductResponse?> GetById(int id)
        {
            return await _productService.GetByIdAsync(id);
        }
    }
}
