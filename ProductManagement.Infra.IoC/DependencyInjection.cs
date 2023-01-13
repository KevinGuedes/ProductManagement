using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProductManagement.Infra.IoC
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
        {
            var applicationLayerAssembly = Assembly.Load("ProductManagement.Application");
        }

        private static void RegisterValidators(IServiceCollection services, Assembly applicationLayerAssembly)
           => services.AddAutoMapper(applicationLayerAssembly);
    }
}
