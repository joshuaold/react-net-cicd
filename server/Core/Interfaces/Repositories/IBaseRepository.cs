using Core.Interfaces.Domain;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    /// <summary>
    /// There must only be one repository per aggregate so there is only one central point of access to that aggregate
    /// The AggregateRoot serves as the entry point to all the other objects in the aggregate
    /// In our use case, our aggregate has Product and ProductOption in its boundary
    /// To get a ProductOption, we must go through a Product
    /// This base repository is for simple, reusable CRUD operations for the aggregate root
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IBaseRepository<T> where T: class, IAggregateRoot
    {
        Task<IEnumerable<T>> GetAllAsync();

        Task<T> GetByIdAsync(Guid id);

        Task UpdateAsync(T entity);
    }
}
