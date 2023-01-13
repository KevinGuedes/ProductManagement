using MediatR;
using ProductManagement.Domain.Entities;
using System.Collections.Generic;

namespace ProductManagement.Application.Products.Queries
{
    public class GetProductsQuery : IRequest<IEnumerable<Product>>
    {
    }
}
