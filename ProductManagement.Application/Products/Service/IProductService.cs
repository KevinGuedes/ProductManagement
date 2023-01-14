﻿using ProductManagement.Application.DTOs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProductManagement.Application.Products.Service
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken cancellationToken);
        Task<ProductDto> GetProductByCodeAsync(int code, CancellationToken cancellationToken);
        Task DeleteProductByCodeAsync(int code, CancellationToken cancellationToken);
        Task<ProductDto> CreateProductAsync(ProductDto productDto, CancellationToken cancellationToken);
        Task<ProductDto> UpdateProductAsync(ProductDto productDto, CancellationToken cancellationToken);
    }
}
