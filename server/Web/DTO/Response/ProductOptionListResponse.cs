using Core.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.DTO.Response
{
    public class ProductOptionListResponse
    {
        public ProductOptionListResponse(IEnumerable<ProductOption> productOptions)
        {
            Items = productOptions.Select(productOption => new ProductOptionResponse(productOption));
        }

        public IEnumerable<ProductOptionResponse> Items { get; set; }
    }
}
