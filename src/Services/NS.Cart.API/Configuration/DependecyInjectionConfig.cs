using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NS.Cart.API.Data;
using NS.WebApi.Core.User;

namespace NS.Cart.API.Configuration
{
    public static class DependecyInjectionConfig
    {
        public static void RegisterService(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddScoped<CartContext>();
        }
    }
}
