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
using eCommerce.Model.Requests;
using MapsterMapper;
using eCommerce.Services.ProductStateMachine;
namespace eCommerce.Services
{
    public class ProductService : BaseCRUDService<ProductResponse, ProductSearchObject, Database.Product, ProductInsertRequest, ProductUpdateRequest>, IProductService
    {
        protected readonly BaseProductState _baseProductState;
        public ProductService(eCommerceDbContext context, IMapper mapper, BaseProductState baseProductState) : base(context, mapper)
        {
            _baseProductState = baseProductState;
        }
        protected override IQueryable<Database.Product> ApplyFilter(IQueryable<Database.Product> query, ProductSearchObject search)
        {
            if(!string.IsNullOrEmpty(search.FTS))
            {
                query = query.Where(p => p.Name.Contains(search.FTS) || p.Description.Contains(search.FTS));
            }

            return query;
        }
        public override async Task<ProductResponse> CreateAsync(ProductInsertRequest request)
        {
            var baseState = _baseProductState.GetProductState("InitialProductState");
            var result = await baseState.CreateAsync(request);

            return result;
            //return base.CreateAsync(request);
        }
        public override async Task<ProductResponse?> UpdateAsync(int id, ProductUpdateRequest request)
        {
            var entity = await _context.Products.FindAsync(id);
            var baseState = _baseProductState.GetProductState(entity.ProductState);
            return await baseState.UpdateAsync(id, request);
            //return base.UpdateAsync(id, request);
        }

        public async Task<ProductResponse> ActivateAsync(int id)
        {
            var entity = await _context.Products.FindAsync(id);
            var baseState = _baseProductState.GetProductState(entity.ProductState);

            return await baseState.ActivateAsync(id);
        }

        public async Task<ProductResponse> DeactivateAsync(int id)
        {
            var entity = await _context.Products.FindAsync(id);
            var baseState = _baseProductState.GetProductState(entity.ProductState);
            return await baseState.DeactivateAsync(id);
        }
    }
}
