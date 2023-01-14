using ProductManagement.Domain.Entities;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProductManagement.Domain.Interfaces
{
    public interface IEntityRepository<TEntity> where TEntity : Entity
    {
        Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken);
        Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);
        Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken);
        void Update(TEntity entity);
    }
}
