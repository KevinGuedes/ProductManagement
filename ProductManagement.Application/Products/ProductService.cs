﻿using AutoMapper;
using FluentResults;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Products.Service;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Enums;
using ProductManagement.Domain.Interfaces;
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

            if (product is null)
                return Result.Fail(new Error("Product not found").WithMetadata("Product Code", code));

            return _mapper.Map<ProductDto>(product);
        }

        public async Task<Result<ProductDto>> CreateProductAsync(CreateProductDto createProductDto, CancellationToken cancellationToken)
        {
            var existingProduct = await _productRepository.GetProductByCodeAsync(createProductDto.Code, cancellationToken);

            if (existingProduct is not null)
                return Result.Fail(new Error("There is already a product with that code").WithMetadata("Product Code", createProductDto.Code));

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
            return Result.Ok(_mapper.Map<ProductDto>(createdProduct));
        }

        public async Task<Result> DeleteProductByCodeAsync(int code, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetProductByCodeAsync(code, cancellationToken);

            if (product is null)
                return Result.Fail(new Error("Product not found").WithMetadata("Product Code", code));

            product.UpdateStatus(ProductStatus.Inactive);
            _productRepository.Update(product);
            await _unitOfWork.CommitChangesAsync(cancellationToken);

            return Result.Ok();
        }

        public async Task<Result<ProductDto>> UpdateProductAsync(UpdateProductDto updateProductDto, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(updateProductDto.Id, cancellationToken);

            if (product is null)
                return Result.Fail(new Error("Product not found").WithMetadata("Product Id", updateProductDto.Id));

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
            return Result.Ok(_mapper.Map<ProductDto>(product));
        }
    }
}
