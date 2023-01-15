using ProductManagement.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ProductManagement.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetProductByCodeAsync(int code, CancellationToken cancellationToken);
    }
}
