using Microsoft.AspNetCore.Mvc.Razor;
using System.Text;
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

        public static string UnitsByProducts(this RazorPage razor, int units)
        {
            return units > 1 ? $"{units} unidades" : $"{units} unidade";
        }

        public static string SelectOptionsByQuantity(this RazorPage razor, int quantity, int valueSelected = 0)
        {
            var sb = new StringBuilder();
            for (int i = 1; i <= quantity; i++)
            {
                var selected = "";
                if (i == valueSelected) selected = "selected";
                sb.Append($"<option {selected} value='{i}'>{i}</option>");
            }

            return sb.ToString();
        }
    }
}
