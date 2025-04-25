using eCommerce.Model.Exceptions;
using eCommerce.Model.Requests;
using eCommerce.Model.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Services.ProductStateMachine
{
    public class BaseProductState
    {
        protected readonly IServiceProvider _serviceProvider;
        public BaseProductState(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
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
                case "Draft":
                    return _serviceProvider.GetService<DraftProductState>();
                default:
                    throw new UserException("Nepoznata akcija");
            }
        }
    }
}
