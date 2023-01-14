using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProductManagement.Application.Products.Service;
using ProductManagement.Application.Validators;
using ProductManagement.Domain.Interfaces;
using ProductManagement.Infra.Persistence.Context;
using ProductManagement.Infra.Persistence.Repositories;
using ProductManagement.Infra.Persistence.UnitOfWork;
using System.Reflection;

namespace ProductManagement.Infra.IoC
{
    public static class DependencyInjection
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var applicationLayerAssembly = Assembly.Load("ProductManagement.Application");
            AddFluentValidation(services, applicationLayerAssembly);
            AddMappers(services, applicationLayerAssembly);
            AddData(services, configuration);
            AddServices(services);
        }

        private static void AddFluentValidation(IServiceCollection services, Assembly applicationLayerAssembly)
        {
            ValidatorOptions.Global.LanguageManager.Enabled = false;
            services
                .AddValidatorsFromAssembly(applicationLayerAssembly)
                .AddFluentValidationAutoValidation();
        }

        private static void AddServices(IServiceCollection services)
        {
            services.AddScoped<IProductService, ProductService>();
        }

        private static void AddData(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddDbContext<ProductManagementContext>(options =>
            //    options.UseInMemoryDatabase("ProductManagement"));
            services
             .AddDbContext<ProductManagementContext>(
                 options => options.UseSqlServer(configuration.GetConnectionString("ProductManagementDb"),
                 m => m.MigrationsAssembly(typeof(ProductManagementContext).Assembly.FullName)));

            services
                .AddScoped<IUnitOfWork, UnitOfWork>()
                .AddScoped<IProductRepository, ProductRepository>();
        }

        private static void AddMappers(IServiceCollection services, Assembly applicationLayerAssembly)
        {
            services.AddAutoMapper(applicationLayerAssembly);
        }
    }
}
