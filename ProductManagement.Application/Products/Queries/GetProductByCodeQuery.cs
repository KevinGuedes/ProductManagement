using MediatR;
using ProductManagement.Domain.Entities;

namespace ProductManagement.Application.Products.Queries
{
    public class GetProductByCodeQuery : IRequest<Product>
    {
        public int Code { get; set; }

        public GetProductByCodeQuery(int code)
        {
            Code = code;
        }
    }
}
