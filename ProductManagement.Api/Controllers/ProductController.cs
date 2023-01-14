using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Products.Service;
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
        public async Task<IEnumerable<ProductDto>> GetProducts(CancellationToken cancellationToken)
        {
            return await _productService.GetAllAsync(cancellationToken);
        }

        [HttpGet("{code:int}")]
        public async Task<ProductDto> GetProductByCode(int code, CancellationToken cancellationToken)
        {
            return await _productService.GetProductByCodeAsync(code, cancellationToken);
        }

        [HttpPost]
        public async Task<ProductDto> CreateProduct(ProductDto product, CancellationToken cancellationToken)
        {
            return await _productService.CreateProductAsync(product, cancellationToken);
        }

        [HttpPut]
        public async Task<IEnumerable<ProductDto>> UpdateProduct()
        {
            throw new System.NotSupportedException();
        }

        [HttpDelete]
        public async Task DeleteProduct()
        {
            throw new System.NotSupportedException();
        }
    }
}
