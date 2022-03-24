using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Domain
{
    /// <summary>
    /// An aggregate is basically a group of related objects
    /// An aggregate root is a specific entity among these objects from which all other objects reference to
    /// In our case, Product and ProductOption are related and part of the same group
    /// But ProductOption holds a reference to a specific Product since you could say that it is a part of the product
    /// This makes Product the aggregate root because to get a ProductOption, you would need the Product
    /// </summary>
    public interface IAggregateRoot
    {
    }
}
