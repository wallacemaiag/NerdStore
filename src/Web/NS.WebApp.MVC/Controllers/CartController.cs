using Microsoft.AspNetCore.Mvc;
using NS.WebApi.Core.User;
using NS.WebApp.MVC.Models;
using NS.WebApp.MVC.Services;
using System;
using System.Threading.Tasks;

namespace NS.WebApp.MVC.Controllers
{
    public class CartController : MainController
    {

        private readonly ICatalogService _catalogService;
        private readonly ICartService _cartService;
        private readonly IAspNetUser _user;

        public CartController(ICatalogService catalogService, ICartService cartService, IAspNetUser user)
        {
            _catalogService = catalogService;
            _cartService = cartService;
            _user = user;
        }

        [Route("cart")]
        public async Task<IActionResult> Index()
        {
            return View(await _cartService.GetCart());
        }

        [HttpPost]
        [Route("cart/add-item")]
        public async Task<IActionResult> AddItemCart(ProductItemViewModel item)
        {
            if (!_user.IsAuthenticated())
            {
                return RedirectToAction("Login", "Identity");
            }

            var product = await _catalogService.GetById(item.ProductId);

            ValidateCart(product, item.Quantity);

            if (!ValidOperation()) return View("Index", await _cartService.GetCart());

            item.Name = product.Name;
            item.Value = product.Value;
            item.Image = product.Image;

            var response = await _cartService.AddItemCart(item);

            if (HasErrorsResponse(response)) return View("Index", await _cartService.GetCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("cart/update-item")]
        public async Task<IActionResult> UpdateItemCart(Guid productId, int quantity)
        {
            var product = await _catalogService.GetById(productId);

            ValidateCart(product, quantity);
            if (!ValidOperation()) return View("Index", await _cartService.GetCart());

            var productItem = new ProductItemViewModel { ProductId = productId, Quantity = quantity };

            var response = await _cartService.UpdateCartItem(productId, productItem);

            if (HasErrorsResponse(response)) return View("Index", await _cartService.GetCart());

            return RedirectToAction("Index");
        }

        [HttpPost]
        [Route("cart/remove-item")]
        public async Task<IActionResult> RemoveItemCart(Guid productId)
        {
            var product = await _catalogService.GetById(productId);

            if (product == null)
            {
                AddErrorValidation("Produto Inexistente!");
                return View("Index", await _cartService.GetCart());
            }

            var response = await _cartService.RemoveCartItem(productId);

            if (HasErrorsResponse(response)) return View("Index", await _cartService.GetCart());

            return RedirectToAction("Index");
        }

        private void ValidateCart(ProductViewModel product, int quantity)
        {
            if (product == null) AddErrorValidation("Produto Inexistente!");
            if (quantity < 1) AddErrorValidation($"Escolha ao menos uma unidade do produto {product.Name}");
            if (quantity > product.StockQuantity) AddErrorValidation($"O produto {product.Name} possí {product.StockQuantity} unidades em estoque!");
        }
    }
}
