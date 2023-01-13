using ProductManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProductManagement.Domain.Interfaces
{
    public interface IEntityRepository<TEntity> where TEntity : Entity
    {
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<TEntity> Insert(TEntity entity, CancellationToken cancellationToken);
        Task<TEntity> Update(TEntity entity, CancellationToken cancellationToken);
    }
}
