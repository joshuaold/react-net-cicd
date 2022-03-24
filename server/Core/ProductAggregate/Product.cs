using Core.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ProductAggregate
{
    /// <summary>
    /// See explanation on IAggregateRoot on why Product has this interface
    /// </summary>
    public class Product : IEntity<Guid>, IAggregateRoot
    {
        public Product()
        {

        }

        public Product(string name, string description, decimal price, decimal deliveryPrice)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            Price = price;
            DeliveryPrice = deliveryPrice;
        }

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal DeliveryPrice { get; set; }

        public void UpdateProductInformation(string name, string description, decimal price, decimal deliveryPrice)
        {
            Name = name;
            Description = description;
            Price = price;
            DeliveryPrice = deliveryPrice;
        }
    }
}
