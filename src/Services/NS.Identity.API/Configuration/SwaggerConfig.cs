using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace NS.Identity.API.Configuration
{
    public static class SwaggerConfig
    {
        public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "NerdStore Identity API",
                    Description = "Essa api faz parte da identidade do projeto NerdStore",
                    Contact = new OpenApiContact() { Name = "Wallace Maia", Email = "wallacemaiag@gmail.com" },
                    License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://www.wallacemaia.com.br") }
                });
            });

            return services;
        }

        public static IApplicationBuilder UseSwagerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "NerdStore Identity API v1"));

            return app;
        }
    }
}
