using Core.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.ProductAggregate
{
    /// <summary>
    /// ProductOption might not be an Entity in this case as it looks to be more of a ValueObject
    /// Thinking about it a little bit more, it looks like it's a value object
    /// This will need some business clarification as mentioned in IAggregateRoot
    /// But for this example, we will leave it as an Entity
    /// </summary>
    public class ProductOption : IEntity<Guid>
    {
        public ProductOption()
        {

        }

        public ProductOption(Guid productId, string name, string description)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            Name = name;
            Description = description;
        }

        public Guid Id { get; set; }

        public Guid ProductId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public void UpdateOptionInformation(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}
