﻿using Microsoft.Extensions.DependencyInjection;
using NS.Catalog.API.Data;
using NS.Catalog.API.Data.Repository;
using NS.Catalog.API.Models;

namespace NS.Catalog.API.Configuration
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<CatalogContext>();
        }
    }
}
