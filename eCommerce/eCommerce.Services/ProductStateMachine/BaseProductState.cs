using eCommerce.Model.Exceptions;
using eCommerce.Model.Requests;
using eCommerce.Model.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using eCommerce.Services.Database;
using MapsterMapper;

namespace eCommerce.Services.ProductStateMachine
{
    public class BaseProductState
    {
        protected readonly IServiceProvider _serviceProvider;
        protected readonly eCommerceDbContext _context;
        protected readonly IMapper _mapper;
        public BaseProductState(IServiceProvider serviceProvider, eCommerceDbContext context, IMapper mapper) 
        {
            _serviceProvider = serviceProvider;
            _context = context;
            _mapper = mapper;
        }

        public virtual async Task<ProductResponse> CreateAsync(ProductInsertRequest request)
        {
            throw new UserException("Nedozvoljena akcija");
        }
        public virtual async Task<ProductResponse> UpdateAsync(int id, ProductUpdateRequest request)
        {
            throw new UserException("Nedozvoljena akcija");
        }
        public virtual async Task<ProductResponse> ActivateAsync(int id)
        {
            throw new UserException("Nedozvoljena akcija");
        }
        public virtual async Task<ProductResponse> DeactivateAsync(int id)
        {
            throw new UserException("Nedozvoljena akcija");
        }
        public BaseProductState GetProductState(string stateName)
        {
            switch (stateName)
            {
                case nameof(InitialProductState):
                    return _serviceProvider.GetService<InitialProductState>();
                case nameof(DraftProductState):
                    return _serviceProvider.GetService<DraftProductState>();
                case nameof(ActiveProductState):
                    return _serviceProvider.GetService<ActiveProductState>();
                case nameof(DeactivatedProductState):
                    return _serviceProvider.GetService<DeactivatedProductState>();
                default:
                    throw new UserException("Nepoznata akcija");
            }
        }
    }
}
