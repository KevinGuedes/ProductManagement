using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Products.Service;
using Swashbuckle.AspNetCore.Annotations;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProductManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [EnableQuery(PageSize = 10)]
        [SwaggerOperation(Summary = "Lists all product according to query parameters")]
        [SwaggerResponse(StatusCodes.Status200OK, "List of all products fetched according to query", typeof(IEnumerable<ProductDto>))]
        public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
        {
            return Ok(await _productService.GetAllAsync(cancellationToken));
        }

        [HttpGet("{code:int}")]
        [SwaggerOperation(Summary = "Get a product by its code")]
        [SwaggerResponse(StatusCodes.Status200OK, "Fetched product", typeof(ProductDto))]
        public async Task<IActionResult> GetProductByCode(int code, CancellationToken cancellationToken)
        {
            return Ok(await _productService.GetProductByCodeAsync(code, cancellationToken));
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new product")]
        [SwaggerResponse(StatusCodes.Status201Created, "Created product", typeof(ProductDto))]
        public async Task<IActionResult> CreateProduct(ProductDto productDto, CancellationToken cancellationToken)
        {
            var createdProduct = await _productService.CreateProductAsync(productDto, cancellationToken);
            return StatusCode(StatusCodes.Status201Created, createdProduct);
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Update an existing product")]
        [SwaggerResponse(StatusCodes.Status200OK, "Updated product", typeof(ProductDto))]
        public async Task<IActionResult> UpdateProduct(ProductDto productDto, CancellationToken cancellationToken)
        {
            var updatedProduct = await _productService.UpdateProductAsync(productDto, cancellationToken);
            return Ok(updatedProduct);
        }

        [HttpDelete("{code:int}")]
        [SwaggerOperation(Summary = "Delete a product by its code")]
        [SwaggerResponse(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteProduct(int code, CancellationToken cancellationToken)
        {
            await _productService.DeleteProductByCodeAsync(code, cancellationToken);
            return Ok();
        }
    }
}
