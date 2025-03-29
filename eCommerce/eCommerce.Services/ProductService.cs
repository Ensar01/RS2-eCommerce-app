using eCommerce.Model;
using eCommerce.Model.SearchObjects;
using eCommerce.Model.Responses;
using eCommerce.Services.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services
{
    public class ProductService : IProductService
    {
        private readonly eCommerceDbContext _context;

        public ProductService(eCommerceDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductResponse>> GetAsync(ProductSearchObject search)
        {
            var query = _context.Products.AsQueryable();
            
            if (!string.IsNullOrEmpty(search.Code))
            {
                query = query.Where(p => p.SKU.Contains(search.Code));
            }
            
            if (!string.IsNullOrEmpty(search.CodeGTE))
            {
                query = query.Where(p => p.SKU.StartsWith(search.CodeGTE));
            }
            
            if (!string.IsNullOrEmpty(search.FTS))
            {
                query = query.Where(p => 
                    (p.SKU != null && p.SKU.Contains(search.FTS)) || 
                    p.Name.Contains(search.FTS) ||
                    p.Description.Contains(search.FTS));
            }
            
            var products = await query.ToListAsync();
            return products.Select(MapToResponse).ToList();
        }

        public async Task<ProductResponse?> GetByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id);
            return product != null ? MapToResponse(product) : null;
        }

        private ProductResponse MapToResponse(Database.Product product)
        {
            return new ProductResponse
            {
                Id = product.Id,
                Name = product.Name,
                Code = product.SKU ?? string.Empty
            };
        }
    }
}
