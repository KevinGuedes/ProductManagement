using AutoMapper;
using MediatR;
using ProductManagement.Application.DTOs;
using ProductManagement.Application.Products.Commands;
using ProductManagement.Application.Products.Queries;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProductManagement.Application.Products.Service
{
    public class ProductService : IProductService
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public ProductService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var products = await _mediator.Send(new GetProductsQuery(), cancellationToken);
            return _mapper.Map<IEnumerable<ProductDto>>(products);
        }

        public Task<ProductDto> GetProductByCodeAsync(int code, CancellationToken cancellationToken)
        {
            throw new System.NotImplementedException();
        }

        public async Task<ProductDto> CreateProductAsync(ProductDto product, CancellationToken cancellationToken)
        {
            var createProductCommand = _mapper.Map<CreateProductCommand>(product);
            var createdProduct = await _mediator.Send(createProductCommand, cancellationToken);
            return _mapper.Map<ProductDto>(createdProduct);
        }
    }
}
