using Core.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Core.Interfaces.Repositories
{
    /// <summary>
    /// See explanation on IBaseRepository on why ProductOption CRUD operations are here
    /// But in summary, Product serves as the entrypoint to ProductOption
    /// Custom operations to the DB for all objects in the Product aggregate go here
    /// </summary>
    public interface IProductRepository : IBaseRepository<Product>
    {
        Task<IEnumerable<Product>> GetProductsByNameAsync(string name);

        Task InsertProductAsync(Product product);

        Task<IEnumerable<ProductOption>> GetProductOptionsByProductIdAsync(Guid productId);

        Task<ProductOption> GetProductOptionsByProductOptionIdAsync(Guid productOptionId);

        Task InsertProductOptionAsync(ProductOption productOption);

        Task UpdateProductOptionAsync(ProductOption productOption);

        Task DeleteProductOptionAsync(Guid productOptionId);

        Task DeleteProduct(Guid productId);
    }
}
