using Core.Interfaces.Repositories;
using Core.ProductAggregate;
using Dapper;
using Infrastructure.Connections;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Infrastructure.Repositories
{
    public class ProductRepository : BaseRepository<Product>, IProductRepository
    {
        public ProductRepository(IConnection connection) : base(connection)
        {

        }

        public async Task<IEnumerable<Product>> GetProductsByNameAsync(string name)
        {
            var openConnection = GetOpenConnection();
            using (openConnection)
            {
                var nameWithWildCard = $"%{name}%";
                var query = $@"SELECT * FROM Product WHERE Name LIKE @nameWithWildCard"; //TODO: change * to the columns that are actually needed
                return await openConnection.QueryAsync<Product>(query, new { nameWithWildCard });
            }
        }

        public async Task InsertProductAsync(Product product)
        {
            // we could have used Dapper's InsertAsync but it requires the Key attribute to be added in the Product entity if we want an Id to be provided by us
            // the reason being is that Dapper assumes that Id is an identity (auto-generated) unless the Key attribute is present
            // the Product entity is in Core, and we don't want to add an infra-related dependency in Core hency why we create a custom method in the ProductRepository instead
            // a solution to this issue would be to create DTOs but it would complicate it too much for this exercise hence we go for this approach
            var openConnection = GetOpenConnection();
            using (openConnection)
            {
                var query = $@"INSERT INTO Product(Id, Name, Description, Price, DeliveryPrice) VALUES(@id, @name, @description, @price, @deliveryPrice)";
                await openConnection.ExecuteAsync(query, new { product.Id, product.Name, product.Description, product.Price, product.DeliveryPrice });
            }
        }

        public async Task<IEnumerable<ProductOption>> GetProductOptionsByProductIdAsync(Guid productId)
        {
            var openConnection = GetOpenConnection();
            using (openConnection)
            {
                var query = $@"SELECT * FROM ProductOption WHERE ProductId = @productId"; //TODO: change * to the columns that are actually needed
                return await openConnection.QueryAsync<ProductOption>(query, new { productId });
            }
        }

        public async Task<ProductOption> GetProductOptionsByProductOptionIdAsync(Guid productOptionId)
        {
            var openConnection = GetOpenConnection();
            using (openConnection)
            {
                var query = $@"SELECT * FROM ProductOption WHERE Id = @productOptionId"; //TODO: change * to the columns that are actually needed
                return await openConnection.QueryFirstOrDefaultAsync<ProductOption>(query, new { productOptionId });
            }
        }

        public async Task InsertProductOptionAsync(ProductOption productOption)
        {
            var openConnection = GetOpenConnection();
            using (openConnection)
            {
                var query = $@"INSERT INTO ProductOption(Id, ProductId, Name, Description) VALUES(@id, @productId, @name, @description)";
                await openConnection.ExecuteAsync(query, new { productOption.Id, productOption.ProductId, productOption.Name, productOption.Description });
            }
        }

        public async Task UpdateProductOptionAsync(ProductOption productOption)
        {
            var openConnection = GetOpenConnection();
            using (openConnection)
            {
                var query = $@"UPDATE ProductOption SET Name = @name, Description = @description WHERE Id = @id";
                await openConnection.ExecuteAsync(query, new { productOption.Name, productOption.Description, productOption.Id });
            }
        }

        public async Task DeleteProductOptionAsync(Guid productOptionId)
        {
            var openConnection = GetOpenConnection();
            using (openConnection)
            {
                var query = $@"DELETE FROM ProductOption WHERE Id = @productOptionId";
                await openConnection.ExecuteAsync(query, new { productOptionId });
            }
        }

        public async Task DeleteProduct(Guid productId)
        {
            var connection = GetOpenConnection();
            using (connection)
            {
                using (var transaction = connection.BeginTransaction())
                {
                    var deleteProductOptionsQuery = $@"DELETE FROM ProductOption WHERE ProductId = @productId";
                    await connection.ExecuteAsync(deleteProductOptionsQuery, new { productId }, transaction: transaction);

                    var deleteProductQuery = $@"DELETE FROM Product WHERE Id = @productId";
                    await connection.ExecuteAsync(deleteProductQuery, new { productId }, transaction: transaction);

                    transaction.Commit();
                }
            }
        }
    }
}
