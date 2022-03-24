using Core.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.DTO.Response
{
    public class ProductListResponse
    {
        public ProductListResponse(IEnumerable<Product> products)
        {
            Items = products.Select(product => new ProductResponse(product));
        }

        public IEnumerable<ProductResponse> Items { get; set; }
    }
}
