using ProductManagement.Domain.Interfaces;
using ProductManagement.Infra.Persistence.Context;
using System.Threading;
using System.Threading.Tasks;

namespace ProductManagement.Infra.Persistence.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ProductManagementContext _productManagementContext;

        public UnitOfWork(ProductManagementContext productManagementContext)
        {
            _productManagementContext = productManagementContext;
        }

        public async Task CommitChangesAsync(CancellationToken cancellationToken)
        {
            await _productManagementContext.SaveChangesAsync();
        }
    }
}
