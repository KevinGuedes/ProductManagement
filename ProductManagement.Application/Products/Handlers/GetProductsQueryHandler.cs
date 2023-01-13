using MediatR;
using Microsoft.Extensions.Logging;
using ProductManagement.Application.Products.Queries;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProductManagement.Application.Products.Handlers
{
    public class GetProductsQueryHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        private readonly ILogger<GetProductsQueryHandler> _logger;
        private readonly IProductRepository _productRepository;

        public GetProductsQueryHandler(ILogger<GetProductsQueryHandler> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving products from database");
            var products = await _productRepository.GetAllAsync(cancellationToken);

            _logger.LogInformation("Products successfully retrieved from database");
            return products;
        }
    }
}
