using eCommerce.Model.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace eCommerce.Model.Messages
{
    public class ProductUpdated
    {
        public ProductResponse Product { get; set; }
    }
}
