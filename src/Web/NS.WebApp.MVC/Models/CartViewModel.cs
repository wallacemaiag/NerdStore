using System.Collections.Generic;

namespace NS.WebApp.MVC.Models
{
    public class CartViewModel
    {
        public decimal Amount { get; set; }
        public List<ProductItemViewModel> Itens { get; set; } = new List<ProductItemViewModel>();
    }
}
