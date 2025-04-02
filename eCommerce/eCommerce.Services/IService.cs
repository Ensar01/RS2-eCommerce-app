using eCommerce.Model.Requests;
using eCommerce.Model.Responses;
using eCommerce.Model.SearchObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services
{
    public interface IService<T, TSearch> where T : class where TSearch: class
    {
        Task<List<T>> GetAsync(TSearch search);
        Task<T?> GetByIdAsync(int id);
     
    }
}
