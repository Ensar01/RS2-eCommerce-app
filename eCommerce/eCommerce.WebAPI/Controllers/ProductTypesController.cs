using eCommerce.Model.Requests;
using eCommerce.Model.Responses;
using eCommerce.Model.SearchObjects;
using eCommerce.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eCommerce.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductTypesController : ControllerBase
    {
        private readonly IProductTypeService _productTypeService;

        public ProductTypesController(IProductTypeService productTypeService)
        {
            _productTypeService = productTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductTypeResponse>>> Get([FromQuery] ProductTypeSearchObject? search = null)
        {
            return await _productTypeService.GetAsync(search ?? new ProductTypeSearchObject());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductTypeResponse>> GetById(int id)
        {
            var productType = await _productTypeService.GetByIdAsync(id);
            
            if (productType == null)
                return NotFound();
                
            return productType;
        }

        [HttpPost]
        public async Task<ActionResult<ProductTypeResponse>> Create(ProductTypeUpsertRequest request)
        {
            var createdProductType = await _productTypeService.CreateAsync(request);
            return CreatedAtAction(nameof(GetById), new { id = createdProductType.Id }, createdProductType);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ProductTypeResponse>> Update(int id, ProductTypeUpsertRequest request)
        {
            var updatedProductType = await _productTypeService.UpdateAsync(id, request);
            
            if (updatedProductType == null)
                return NotFound();
                
            return updatedProductType;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var deleted = await _productTypeService.DeleteAsync(id);
            
            if (!deleted)
                return NotFound();
                
            return NoContent();
        }
    }
} 