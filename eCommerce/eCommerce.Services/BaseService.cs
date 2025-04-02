using eCommerce.Model.Requests;
using eCommerce.Model.Responses;
using eCommerce.Model.SearchObjects;
using eCommerce.Services.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services
{
    public class BaseService<T, TSearch> : IService<T, TSearch> where T: class where TSearch : class
    {
        private readonly eCommerceDbContext _context;

        public BaseService(eCommerceDbContext context)
        {
            _context = context;
        }
        {
            throw new NotImplementedException();
        }

        public virtual async Task<List<T?>> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
        public async Task<List<ProductTypeResponse>> GetAsync(ProductTypeSearchObject search)
        {
            var query = _context.ProductTypes.AsQueryable();

            if (!string.IsNullOrEmpty(search.Name))
            {
                query = query.Where(pt => pt.Name.Contains(search.Name));
            }

            if (!string.IsNullOrEmpty(search.FTS))
            {
                query = query.Where(pt =>
                    pt.Name.Contains(search.FTS) ||
                    (pt.Description != null && pt.Description.Contains(search.FTS)));
            }

            var productTypes = await query.ToListAsync();
            return productTypes.Select(MapToResponse).ToList();
        }

        public async Task<ProductTypeResponse?> GetByIdAsync(int id)
        {
            var productType = await _context.ProductTypes.FindAsync(id);
            return productType != null ? MapToResponse(productType) : null;
        }

        public async Task<ProductTypeResponse> CreateAsync(ProductTypeUpsertRequest request)
        {
            // Check for duplicate name
            if (await _context.ProductTypes.AnyAsync(pt => pt.Name == request.Name))
            {
                throw new InvalidOperationException("A product type with this name already exists.");
            }

            var productType = new ProductType
            {
                Name = request.Name,
                Description = request.Description,
                IsActive = request.IsActive,
                CreatedAt = DateTime.UtcNow
            };

            _context.ProductTypes.Add(productType);
            await _context.SaveChangesAsync();
            return MapToResponse(productType);
        }

        public async Task<ProductTypeResponse?> UpdateAsync(int id, ProductTypeUpsertRequest request)
        {
            var productType = await _context.ProductTypes.FindAsync(id);
            if (productType == null)
                return null;

            // Check for duplicate name (excluding current product type)
            if (await _context.ProductTypes.AnyAsync(pt => pt.Name == request.Name && pt.Id != id))
            {
                throw new InvalidOperationException("A product type with this name already exists.");
            }

            productType.Name = request.Name;
            productType.Description = request.Description;
            productType.IsActive = request.IsActive;
            productType.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return MapToResponse(productType);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var productType = await _context.ProductTypes.FindAsync(id);
            if (productType == null)
                return false;

            // Check if there are any products using this product type
            if (await _context.Products.AnyAsync(p => p.ProductTypeId == id))
            {
                throw new InvalidOperationException("Cannot delete a product type that is being used by products.");
            }

            _context.ProductTypes.Remove(productType);
            await _context.SaveChangesAsync();
            return true;
        }

        private ProductTypeResponse MapToResponse(ProductType productType)
        {
            return new ProductTypeResponse
            {
                Id = productType.Id,
                Name = productType.Name,
                Description = productType.Description,
                IsActive = productType.IsActive,
                CreatedAt = productType.CreatedAt,
                UpdatedAt = productType.UpdatedAt
            };
        }

       

        
    }
} 

