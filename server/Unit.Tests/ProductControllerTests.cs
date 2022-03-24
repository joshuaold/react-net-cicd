using Core.Interfaces.Repositories;
using Core.ProductAggregate;
using FakeItEasy;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Web.Controllers;
using Web.DTO.Response;
using Xunit;

namespace Unit.Tests
{
    /// <summary>
    /// This is where we would test the endpoints of the ProductController
    /// Only GET endpoints have been done just to showcase sample unit tests for the application
    /// </summary>
    public class ProductControllerTests
    {
        [Fact]
        public async void GetProductsReturnEmptyArrayIfNoItems()
        {
            // Arrange
            var repository = A.Fake<IProductRepository>();
            var products = A.CollectionOfDummy<Product>(0).AsEnumerable();
            A.CallTo(() => repository.GetAllAsync())
                .Returns(Task.FromResult(products));
            var controller = new ProductsController(repository);

            // Fact
            var action = await controller.GetProducts("");

            // Assert
            var result = action as OkObjectResult;
            var returnedProducts = result.Value as ProductListResponse;
            Assert.Empty(returnedProducts.Items);
        }

        [Fact]
        public async void GetProductOptionsReturnEmptyArrayIfNoItems()
        {
            // Arrange
            var repository = A.Fake<IProductRepository>();
            var productOptionss = A.CollectionOfDummy<ProductOption>(0).AsEnumerable();
            var productId = Guid.NewGuid();
            A.CallTo(() => repository.GetProductOptionsByProductIdAsync(productId))
                .Returns(Task.FromResult(productOptionss));
            var controller = new ProductsController(repository);

            // Fact
            var action = await controller.GetProductOptions(productId);

            // Assert
            var result = action as OkObjectResult;
            var returnedProductOptions = result.Value as ProductOptionListResponse;
            Assert.Empty(returnedProductOptions.Items);
        }
    }
}
