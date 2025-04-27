using eCommerce.Model.Requests;
using eCommerce.Model.Responses;
using eCommerce.Services.Database;
using MapsterMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services.ProductStateMachine
{
    public class DeactivatedProductState : BaseProductState
    {
        public DeactivatedProductState(IServiceProvider serviceProvider, eCommerceDbContext eCommerceDbContext, IMapper mapper) : base(serviceProvider, eCommerceDbContext, mapper)
        {
        }
        public override async Task<ProductResponse> UpdateAsync(int id, ProductUpdateRequest request)
        {
            var entity = await _context.Products.FindAsync(id);
            entity.ProductState = nameof(DraftProductState);

            await _context.SaveChangesAsync();
            return _mapper.Map<ProductResponse>(entity);
        }
    }
}
