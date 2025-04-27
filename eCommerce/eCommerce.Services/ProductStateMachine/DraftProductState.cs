
using eCommerce.Model.Requests;
using eCommerce.Model.Responses;
using eCommerce.Services.Database;
using MapsterMapper;

namespace eCommerce.Services.ProductStateMachine
{
    public class DraftProductState : BaseProductState
    {
        public DraftProductState(IServiceProvider serviceProvider, eCommerceDbContext eCommerceDbContext, IMapper mapper) : base(serviceProvider, eCommerceDbContext, mapper)
        {
        }
        public override async Task<ProductResponse> UpdateAsync(int id, ProductUpdateRequest request)
        {
            var entity = await _context.Products.FindAsync(id);
            _mapper.Map(request, entity);

            await _context.SaveChangesAsync();
            return _mapper.Map<ProductResponse>(entity);
        }
        public override async Task<ProductResponse> ActivateAsync(int id)
        {
            var entity =await _context.Products.FindAsync(id);
            entity.ProductState = nameof(ActiveProductState);

            await _context.SaveChangesAsync();
            return _mapper.Map<ProductResponse>(entity);
        }
        public override async Task<ProductResponse> DeactivateAsync(int id)
        {
            var entity = await _context.Products.FindAsync(id);
            entity.ProductState = nameof(DeactivatedProductState);
            entity.Name = entity.Name+ " - Deactivated from draft";
            await _context.SaveChangesAsync();
            return _mapper.Map<ProductResponse>(entity);
        }
    }
}