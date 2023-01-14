using System.Threading;
using System.Threading.Tasks;

namespace ProductManagement.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task CommitChangesAsync(CancellationToken cancellationToken);
    }
}
