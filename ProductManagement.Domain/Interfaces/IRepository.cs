using ProductManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProductManagement.Domain.Interfaces
{
    public interface IRepository<TAggregate> where TAggregate : AggregateRoot
    {
        Task<TAggregate> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<TAggregate>> GetAllAsync(CancellationToken cancellationToken);
        Task<TAggregate> AddAsync(TAggregate aggregateRoot, CancellationToken cancellationToken);
        void Update(TAggregate aggregateRoot);
    }
}
