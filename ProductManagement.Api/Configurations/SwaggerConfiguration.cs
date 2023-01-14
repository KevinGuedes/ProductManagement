using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace MyFinance.Presentation.Configurations
{
    public static class SwaggerConfiguration
    {
        public static void AddCustomSwaggerConfiguration(this IServiceCollection services)
        {
            services
                .AddSwaggerGen(configuration =>
                {
                    configuration.EnableAnnotations();
                    configuration.SwaggerDoc("v1", new OpenApiInfo
                    {
                        Version = "v1",
                        Title = "ProductManagement API",
                        Description = "API for Product Management",
                        License = new OpenApiLicense { Name = "MIT" },
                        Contact = new OpenApiContact
                        {
                            Name = "Kevin Santos Gudes",
                            Email = "kevinguedes1@gmail.com",
                            Url = new Uri("https://www.linkedin.com/in/kevinsantosguedes/")
                        }
                    });
                });
        }

        public static void UseCustomSwaggerConfiguration(this IApplicationBuilder app)
        {
            app
                .UseSwagger()
                .UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductManagement API");
                    options.DocumentTitle = "ProductManagement API";
                });
        }
    }
}
