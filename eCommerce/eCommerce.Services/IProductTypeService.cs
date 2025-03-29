using eCommerce.Services.Database;
using System.Collections.Generic;
using System.Threading.Tasks;
using eCommerce.Model.Responses;
using eCommerce.Model.Requests;
using eCommerce.Model.SearchObjects;

namespace eCommerce.Services
{
    public interface IProductTypeService
    {
        Task<List<ProductTypeResponse>> GetAsync(ProductTypeSearchObject search);
        Task<ProductTypeResponse?> GetByIdAsync(int id);
        Task<ProductTypeResponse> CreateAsync(ProductTypeUpsertRequest request);
        Task<ProductTypeResponse?> UpdateAsync(int id, ProductTypeUpsertRequest request);
        Task<bool> DeleteAsync(int id);
    }
} 