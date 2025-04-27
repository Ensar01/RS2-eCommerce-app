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
    public class ActiveProductState : BaseProductState
    {
        public ActiveProductState(IServiceProvider serviceProvider, eCommerceDbContext eCommerceDbContext, IMapper mapper) : base(serviceProvider, eCommerceDbContext, mapper)
        {
        }

        public override async Task<ProductResponse> DeactivateAsync(int id)
        {
            var entity = await _context.Products.FindAsync(id);
            entity.ProductState = nameof(DeactivatedProductState);

            await _context.SaveChangesAsync();
            return _mapper.Map<ProductResponse>(entity);
        }
    }
}
