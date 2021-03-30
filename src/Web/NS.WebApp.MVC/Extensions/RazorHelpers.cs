using Microsoft.AspNetCore.Mvc.Razor;
using System.Threading;

namespace NS.WebApp.MVC.Extensions
{
    public static class RazorHelpers
    {
        public static string StockMessage(this RazorPage razor, int quantity)
        {
            return quantity > 0 ? $"Apenas {quantity} em estoque!" : "Prdouto esgotado! :(";
        }

        public static string ValueFormat(this RazorPage razor, decimal value)
        {
            return value > 0 ? string.Format(Thread.CurrentThread.CurrentCulture, "{0:C}", value) : "Gratuito";
        }
    }
}
