using eCommerce.Model.Exceptions;
using eCommerce.Model.Requests;
using eCommerce.Model.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Services.ProductStateMachine
{
    public class BaseProductState
    {
        public virtual ProductResponse CreateAsync(ProductInsertRequest request)
        {
            throw new UserException("Nedozvoljena akcija");
        }
    }
}
