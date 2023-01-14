using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Application.Products.Service;
using ProductManagement.Domain.Interfaces;
using ProductManagement.Infra.Persistence.Context;
using ProductManagement.Infra.Persistence.Repositories;
using System.Reflection;

namespace ProductManagement.Infra.IoC
{
    public static class DependencyInjection
    {
        public static void InjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var applicationLayerAssembly = Assembly.Load("ProductManagement.Application");

            RegisterMediatR(services, applicationLayerAssembly);
            RegisterMappers(services, applicationLayerAssembly);
            RegisterData(services, configuration);
            RegisterServices(services);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
        }

        private static void RegisterData(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<ProductManagementContext>(options =>
            //    options.UseInMemoryDatabase("ProductManagement"));
            services
             .AddDbContext<ProductManagementContext>(
                 options => options.UseSqlServer(configuration.GetConnectionString("ProductManagementDb"),
                 m => m.MigrationsAssembly(typeof(ProductManagementContext).Assembly.FullName)));

            services.AddScoped<IProductRepository, ProductRepository>();
        }

        private static void RegisterMappers(IServiceCollection services, Assembly applicationLayerAssembly)
        {
            services.AddAutoMapper(applicationLayerAssembly);
        }

        private static void RegisterMediatR(IServiceCollection services, Assembly applicationLayerAssembly)
        {
            services.AddMediatR(applicationLayerAssembly);
        }
    }
}
