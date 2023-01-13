using MediatR;
using Microsoft.Extensions.Logging;
using ProductManagement.Application.Products.Commands;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ProductManagement.Application.Products.Handlers
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Product>
    {
        private readonly ILogger<CreateProductCommandHandler> _logger;
        private readonly IProductRepository _productRepository;

        public CreateProductCommandHandler(ILogger<CreateProductCommandHandler> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public Task<Product> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Creating new product");
            var product = new Product(
                request.Code,
                request.Description,
                request.Status,
                request.ManufacturingDate,
                request.ExpirationDate,
                request.SupplierCode,
                request.SupplierDescription,
                request.SupplierCNPJ);

            _productRepository.Insert(product);

            _logger.LogInformation("Product successfully created");
            return Task.FromResult(product);
        }
    }
}
