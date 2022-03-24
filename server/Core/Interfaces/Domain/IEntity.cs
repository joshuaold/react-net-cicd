using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Domain
{
    /// <summary>
    /// Entities are essentially objects with an identity
    /// For example, a Product has an identity because it is a specific thing (Samsung Galaxy S2, iPhone 5s)
    /// ProductOption is a bit debatable. It looks like it could be a ValueObject as it only ever serves as an attribute to a Product
    /// This will require discussion with the business to determine if a ProductOption will ever have an identity of its own
    /// But from the looks of it, ProductOption looks to be a ValueObject
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IEntity<T>
    {
        public T Id { get; set; }
    }
}
