using Core.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.DTO.Response
{
    public class ProductResponse
    {
        public ProductResponse(Product product)
        {
            if (product != null)
            {
                Id = product.Id;
                Name = product.Name;
                Description = product.Description;
                Price = product.Price;
                DeliveryPrice = product.DeliveryPrice;
            }
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }
    }
}
