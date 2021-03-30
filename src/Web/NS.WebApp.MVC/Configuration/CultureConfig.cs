using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Localization;
using System.Globalization;

namespace NS.WebApp.MVC.Configuration
{
    public static class CultureConfig
    {
        public static void UseCulture(this IApplicationBuilder app)
        {
            var supportedCulture = new[] { new CultureInfo("pt-BR") };
            app.UseRequestLocalization(new RequestLocalizationOptions
            {
                DefaultRequestCulture = new RequestCulture("pt-BR"),
                SupportedCultures = supportedCulture,
                SupportedUICultures = supportedCulture
            });
        }
    }
}
