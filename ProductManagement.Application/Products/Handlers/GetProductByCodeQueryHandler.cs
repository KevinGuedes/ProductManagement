using MediatR;
using Microsoft.Extensions.Logging;
using ProductManagement.Application.Products.Queries;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace ProductManagement.Application.Products.Handlers
{
    public class GetProductByCodeQueryHandler : IRequestHandler<GetProductByCodeQuery, Product>
    {
        private readonly ILogger<GetProductByCodeQueryHandler> _logger;
        private readonly IProductRepository _productRepository;

        public GetProductByCodeQueryHandler(ILogger<GetProductByCodeQueryHandler> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        public async Task<Product> Handle(GetProductByCodeQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Retrieving product with code {Code} from database", request.Code);
            var product = await _productRepository.GetProductByCodeAsync(request.Code, cancellationToken);

            _logger.LogInformation("Product with code {Code} successfully retrieved from database", request.Code);
            return product;
        }
    }
}
