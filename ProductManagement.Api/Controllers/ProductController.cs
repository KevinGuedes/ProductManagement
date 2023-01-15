using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Products;
using ProductManagement.Application.Validators;
using ProductManagement.Domain.Interfaces;
using ProductManagement.Infra.Persistence.Repositories;
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
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad product parameters", typeof(ValidationProblemDetails))]
        public async Task<IActionResult> CreateProduct(CreateProductDto createProductDto, CancellationToken cancellationToken)
        {
            var validator = new CreateProductDtoValidator();
            var valdiationResult = await validator.ValidateAsync(createProductDto, cancellationToken);

            if (valdiationResult.IsValid)
            {
                var createdProduct = await _productService.CreateProductAsync(createProductDto, cancellationToken);
                return StatusCode(StatusCodes.Status201Created, createdProduct);
            }
            else
            {
                valdiationResult.AddToModelState(ModelState);
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
        }

        [HttpPut]
        [SwaggerOperation(Summary = "Update an existing product")]
        [SwaggerResponse(StatusCodes.Status200OK, "Updated product", typeof(ProductDto))]
        [SwaggerResponse(StatusCodes.Status400BadRequest, "Bad product parameters", typeof(ValidationProblemDetails))]
        public async Task<IActionResult> UpdateProduct([FromServices] IProductRepository productRepository, UpdateProductDto updateProductDto, CancellationToken cancellationToken)
        {
            var validator = new UpdateProductDtoValidator(productRepository);
            var valdiationResult = await validator.ValidateAsync(updateProductDto, cancellationToken);

            if(valdiationResult.IsValid)
            {
                var updatedProduct = await _productService.UpdateProductAsync(updateProductDto, cancellationToken);
                return Ok(updatedProduct);
            }
            else
            {
                valdiationResult.AddToModelState(ModelState);
                return BadRequest(new ValidationProblemDetails(ModelState));
            }
        }

        [HttpDelete("{code:int}")]
        [SwaggerOperation(Summary = "Delete a product by its code")]
        [SwaggerResponse(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteProduct(int code, CancellationToken cancellationToken)
        {
            await _productService.DeleteProductByCodeAsync(code, cancellationToken);
            return NoContent();
        }
    }
}
