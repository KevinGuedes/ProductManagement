using Microsoft.EntityFrameworkCore;
using ProductManagement.Domain.Entities;
using ProductManagement.Domain.Interfaces;
using ProductManagement.Infra.Persistence.Context;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProductManagement.Infra.Persistence.Repositories
{

    public class ProductRepository : IProductRepository
    {
        public readonly ProductManagementContext _productManagementContext;

        public ProductRepository(ProductManagementContext productManagementContext)
        {
            _productManagementContext = productManagementContext;
        }


        public async Task<Product> GetProductByIdAsync(int id, CancellationToken cancellationToken)
        {
            return await _productManagementContext.Products.FindAsync(id, cancellationToken);
        }

        public async Task<IEnumerable<Product>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _productManagementContext.Products.ToListAsync(cancellationToken);
        }

        public async Task<Product> GetProductByCodeAsync(int code, CancellationToken cancellationToken)
        {
            var product = await _productManagementContext
                .Products
                .AsNoTracking()
                .FirstOrDefaultAsync(product => product.Code == code, cancellationToken);

            return product;
        }

        public async Task<Product> AddAsync(Product product, CancellationToken cancellationToken)
        {
            await _productManagementContext.Products.AddAsync(product, cancellationToken);
            await _productManagementContext.SaveChangesAsync(cancellationToken);
            return product;
        }

        public void Update(Product product)
        {
            _productManagementContext.Products.Update(product);
            _productManagementContext.SaveChanges();
        }
    }
}

