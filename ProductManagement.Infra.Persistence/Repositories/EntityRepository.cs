using ProductManagement.Infra.Persistence.Context;

namespace ProductManagement.Infra.Persistence.Repositories
{
    public abstract class EntityRepository
    {
        public readonly ProductManagementContext _productManagementContext;

        protected EntityRepository(ProductManagementContext productManagementContext)
        {
            _productManagementContext = productManagementContext;
        }
    }
}
