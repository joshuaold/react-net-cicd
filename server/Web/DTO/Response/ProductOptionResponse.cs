using Core.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.DTO.Response
{
    public class ProductOptionResponse
    {
        public ProductOptionResponse(ProductOption productOption)
        {
            if (productOption != null)
            {
                Id = productOption.Id;
                Name = productOption.Name;
                Description = productOption.Description;
            }
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
