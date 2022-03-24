using Core.Interfaces.Repositories;
using Core.ProductAggregate;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.DTO.Request;
using Web.DTO.Response;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _productRepository;

        public ProductsController(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// 1. `GET /products` - gets all products.
        /// 2. `GET /products?name={name}` - finds all products matching the specified name.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetProducts([FromQuery] string name)
        {
            /*var products = string.IsNullOrWhiteSpace(name) ? await _productRepository.GetAllAsync() : await _productRepository.GetProductsByNameAsync(name); // TODO: find a better way of making this extensible in case new query parameters are added*/

            var products = new List<Product>()
            {
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product1",
                    Description = "Description1",
                    Price = 1,
                    DeliveryPrice = 11
                },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product2",
                    Description = "Description2",
                    Price = 2,
                    DeliveryPrice = 22
                },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product3",
                    Description = "Description3",
                    Price = 3,
                    DeliveryPrice = 33
                },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product3",
                    Description = "Description3",
                    Price = 3,
                    DeliveryPrice = 33
                },
                new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = "Product",
                    Description = "Description4",
                    Price = 4,
                    DeliveryPrice = 44
                }
            };

            var response = new ProductListResponse(products);
            return Ok(response);
        }

        /// <summary>
        /// 3. `GET /products/{id}` - gets the project that matches the specified ID - ID is a GUID.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(Guid productId)
        {
            var product = await _productRepository.GetByIdAsync(productId);
            // TODO: do some null check here to return an empty JSON {} instead of default ProductResponse values, and same with GetProductOptionByProductOptionId
            var response = new ProductResponse(product);
            return Ok(response);
        }

        /// <summary>
        /// 4. `POST /products` - creates a new product.
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductRequest body)
        {
            var productToAdd = new Product(body.Name, body.Description, body.Price, body.DeliveryPrice);
            await _productRepository.InsertProductAsync(productToAdd);
            return Ok();
        }

        /// <summary>
        /// 5. `PUT /products/{id}` - updates a product.
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPut("{productId}")]
        public async Task<IActionResult> UpdateProduct([FromBody] UpdateProductRequest body)
        {
            // since we update the product using details passed in the request body, using the productId in body instead of the query parameter keeps it uniform
            var existingProduct = await _productRepository.GetByIdAsync(body.Id);

            if (existingProduct == null)
            {
                return NotFound(); // TODO: use custom exceptions here, exceptions should be in Core since they are business logic related
            }

            existingProduct.UpdateProductInformation(body.Name, body.Description, body.Price, body.DeliveryPrice);
            await _productRepository.UpdateAsync(existingProduct);
            return Ok();
        }

        /// <summary>
        /// 6. `DELETE /products/{id}` - deletes a product and its options.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpDelete("{productId}")]
        public async Task<IActionResult> DeleteProduct(Guid productId)
        {
            await _productRepository.DeleteProduct(productId);
            return Ok();
        }

        /// <summary>
        /// 7. `GET /products/{id}/options` - finds all options for a specified product.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        [HttpGet("{productId}/options")]
        public async Task<IActionResult> GetProductOptions(Guid productId)
        {
            var productOptions = await _productRepository.GetProductOptionsByProductIdAsync(productId);
            var response = new ProductOptionListResponse(productOptions);
            return Ok(response);
        }

        /// <summary>
        /// 8. `GET /products/{id}/options/{optionId}` - finds the specified product option for the specified product.
        /// </summary>
        /// <param name="productOptionId"></param>
        /// <returns></returns>
        [HttpGet("{productId}/options/{productOptionId}")]
        public async Task<IActionResult> GetProductOptionByProductOptionId(Guid productOptionId)
        {
            var productOption = await _productRepository.GetProductOptionsByProductOptionIdAsync(productOptionId);
            var response = new ProductOptionResponse(productOption);
            return Ok(response);
        }

        /// <summary>
        /// 9. `POST /products/{id}/options` - adds a new product option to the specified product.
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost("{productId}/options")]
        public async Task<IActionResult> CreateProductOption([FromBody] CreateProductOptionRequest body)
        {
            var productOptionToAdd = new ProductOption(body.ProductId, body.Name, body.Description);
            await _productRepository.InsertProductOptionAsync(productOptionToAdd);
            return Ok();
        }

        /// <summary>
        /// 10. `PUT /products/{id}/options/{optionId}` - updates the specified product option.
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPut("{productId}/options/{productOptionId}")]
        public async Task<IActionResult> UpdateProductOption([FromBody] UpdateProductOptionRequest body)
        {
            var existingProductOption = await _productRepository.GetProductOptionsByProductOptionIdAsync(body.Id);

            if (existingProductOption == null)
            {
                return NotFound(); // TODO: use custom exceptions here, exceptions should be in Core since they are business logic related
            }

            existingProductOption.UpdateOptionInformation(body.Name, body.Description);
            await _productRepository.UpdateProductOptionAsync(existingProductOption);
            return Ok();
        }

        /// <summary>
        /// 11. `DELETE /products/{id}/options/{optionId}` - deletes the specified product option.
        /// </summary>
        /// <param name="productOptionId"></param>
        /// <returns></returns>
        [HttpDelete("{productId}/options/{productOptionId}")]
        public async Task<IActionResult> DeleteProductOption(Guid productOptionId)
        {
            await _productRepository.DeleteProductOptionAsync(productOptionId);
            return Ok();
        }
    }
}
