using FluentResults;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
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
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [SwaggerResponse(StatusCodes.Status500InternalServerError, "Backend went rogue", typeof(ProblemDetails))]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [EnableQuery(PageSize = 5)]
        [SwaggerOperation(Summary = "Lists all product according to query parameters")]
        [SwaggerResponse(StatusCodes.Status200OK, "List of all products fetched according to query", typeof(IEnumerable<ProductDto>))]
        public async Task<IActionResult> GetProducts(CancellationToken cancellationToken)
        {
            return Ok(await _productService.GetAllAsync(cancellationToken));
        }

        [HttpGet("{code:int}")]
        [SwaggerOperation(Summary = "Get a product by its code")]
        [SwaggerResponse(StatusCodes.Status200OK, "Fetched product", typeof(ProductDto))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found", typeof(NotFound<List<IReason>>))]
        public async Task<IActionResult> GetProductByCode(int code, CancellationToken cancellationToken)
        {
            var result = await _productService.GetProductByCodeAsync(code, cancellationToken);

            if (result.IsSuccess)
                return Ok(result.Value);

            return NotFound(result.Reasons);
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Create a new product")]
        [SwaggerResponse(StatusCodes.Status201Created, "Created product", typeof(ProductDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad product parameters", typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status422UnprocessableEntity, "Product can not be created", typeof(UnprocessableEntity<List<IReason>>))]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto, CancellationToken cancellationToken)
        {
            var result = await _productService.CreateProductAsync(createProductDto, cancellationToken);

            if (result.IsSuccess)
                return StatusCode(StatusCodes.Status201Created, result.Value);

            return UnprocessableEntity(result.Reasons);
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Update an existing product")]
        [SwaggerResponse(StatusCodes.Status200OK, "Updated product", typeof(ProductDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad product parameters", typeof(ValidationProblemDetails))]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found", typeof(NotFound<List<IReason>>))]
        public async Task<IActionResult> UpdateProduct(UpdateProductDto updateProductDto, CancellationToken cancellationToken)
        {
            var result = await _productService.UpdateProductAsync(updateProductDto, cancellationToken);

            if (result.IsSuccess)
                return Ok(result.Value);

            return NotFound(result.Reasons);
        }

        [HttpDelete("{code:int}")]
        [SwaggerOperation(Summary = "Delete a product by its code")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        [SwaggerResponse(StatusCodes.Status404NotFound, "Product not found", typeof(NotFound<List<IReason>>))]
        public async Task<IActionResult> DeleteProduct(int code, CancellationToken cancellationToken)
        {
            var result = await _productService.DeleteProductByCodeAsync(code, cancellationToken);

            if (result.IsSuccess)
                return NoContent();

            return NotFound(result.Reasons);
        }
    }
}
