using AutoMapper;
using FluentResults;
using ProductManagement.Application.DTOs;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Enums;
using ProductManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProductManagement.Application.Products
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IMapper mapper, IProductRepository productRepository, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _productRepository = productRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var products = await _productRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public async Task<Result<ProductDto>> GetProductByCodeAsync(int code, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByCodeAsync(code, cancellationToken);
            if(product is null)
                return Result.Fail(new Error("Product not found"));

            return Result.Ok(_mapper.Map<ProductDto>(product));
        }

        public async Task<ProductDto> CreateProductAsync(CreateProductDto createProductDto, CancellationToken cancellationToken)
        {
            var createdProduct = new Product(
                createProductDto.Code,
                createProductDto.Description,
                createProductDto.Status,
                createProductDto.ManufacturingDate,
                createProductDto.ExpirationDate,
                createProductDto.SupplierCode,
                createProductDto.SupplierDescription,
                createProductDto.SupplierCnpj);

            await _productRepository.AddAsync(createdProduct, cancellationToken);
            await _unitOfWork.CommitChangesAsync(cancellationToken);
            return _mapper.Map<ProductDto>(createdProduct);
        }

        public async Task DeleteProductByCodeAsync(int code, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByCodeAsync(code, cancellationToken);
            product.UpdateStatus(ProductStatus.Inactive);
            _productRepository.Update(product);
            await _unitOfWork.CommitChangesAsync(cancellationToken);
        }

        public async Task<ProductDto> UpdateProductAsync(UpdateProductDto updateProductDto, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(updateProductDto.Id, cancellationToken);

            product.Update(
                updateProductDto.Code,
                updateProductDto.Description,
                updateProductDto.Status,
                updateProductDto.ManufacturingDate,
                updateProductDto.ExpirationDate,
                updateProductDto.SupplierCode,
                updateProductDto.SupplierDescription,
                updateProductDto.SupplierCnpj);
            _productRepository.Update(product);
            await _unitOfWork.CommitChangesAsync(cancellationToken);
            return _mapper.Map<ProductDto>(product);
        }
    }
}
