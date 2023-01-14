using AutoMapper;
using ProductManagement.Application.DTOs;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Enums;
using ProductManagement.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProductManagement.Application.Products.Service
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        public ProductService(IMapper mapper, IProductRepository productRepository)
        {
            _mapper = mapper;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<ProductDto> GetProductByCodeAsync(int code, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByCodeAsync(code, cancellationToken);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto productDto, CancellationToken cancellationToken)
        {
            var createdProduct = new Product(
                productDto.Code,
                productDto.Description,
                productDto.Status,
                productDto.ManufacturingDate,
                productDto.ExpirationDate,
                productDto.SupplierCode,
                productDto.SupplierDescription,
                productDto.SupplierCNPJ);

            await _productRepository.AddAsync(createdProduct, cancellationToken);
            return _mapper.Map<ProductDto>(createdProduct);
        }

        public async Task DeleteProductByCodeAsync(int code, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByCodeAsync(code, cancellationToken);
            product.UpdateStatus(ProductStatus.Inactive);
            _productRepository.Update(product);
        }

        public async Task<ProductDto> UpdateProductAsync(ProductDto productDto, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByIdAsync(productDto.Id, cancellationToken);
            product.Update(
                productDto.Code,
                productDto.Description,
                productDto.Status,
                productDto.ManufacturingDate,
                productDto.ExpirationDate,
                productDto.SupplierCode,
                productDto.SupplierDescription,
                productDto.SupplierCNPJ);

            _productRepository.Update(product);
            return _mapper.Map<ProductDto>(product);
        }
    }
}
