using eCommerce.Model.Requests;
using eCommerce.Model.Responses;
using eCommerce.Services.Database;
using MapsterMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services.ProductStateMachine
{
    public class InitialProductState : BaseProductState
    {
       
        public InitialProductState(IServiceProvider serviceProvider, eCommerceDbContext eCommerceDbContext, IMapper mapper) : base(serviceProvider, eCommerceDbContext, mapper)
        {
        }

        public override async Task<ProductResponse> CreateAsync(ProductInsertRequest request)
        {
            var entity = new Database.Product();
            _mapper.Map(request, entity);
            entity.ProductState = nameof(DraftProductState);
            _context.Products.Add(entity);
            await _context.SaveChangesAsync();

            
            return _mapper.Map<ProductResponse>(entity);
        }

       
    }
}
