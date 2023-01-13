using ProductManagement.Domain.Entities;
using System.Threading.Tasks;

namespace ProductManagement.Domain.Interfaces
{
    public interface IProductRepository : IEntityRepository<Product>
    {
        Task<Product> GetProductByCode(int code);
    }
}
