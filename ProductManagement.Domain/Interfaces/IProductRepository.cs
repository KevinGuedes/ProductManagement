using ProductManagement.Domain.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace ProductManagement.Domain.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<Product> GetByCodeAsync(int code, CancellationToken cancellationToken);
    }
}
