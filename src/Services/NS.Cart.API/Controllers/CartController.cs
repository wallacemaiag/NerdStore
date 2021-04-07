using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NS.Cart.API.Data;
using NS.Cart.API.Models;
using NS.WebApi.Core.Controllers;
using NS.WebApi.Core.User;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NS.Cart.API.Controllers
{
    public class CartController : MainController
    {
        private readonly IAspNetUser _user;
        private readonly CartContext _context;

        public CartController(IAspNetUser user, CartContext context)
        {
            _context = context;
            _user = user;
        }

        [HttpGet("cart")]
        public async Task<CustomerCart> GetCart()
        {
            return await GetCustomerCart() ?? new CustomerCart();
        }

        [HttpPost("cart")]
        public async Task<IActionResult> AddCartItem(CartItem item)
        {
            var cart = await GetCustomerCart();

            if (cart == null)
                HandleNewCart(item);
            else
                HandleExistingCart(item, cart);

            if (!ValidOperation()) return CustomResponse();

            await PersistData();

            return CustomResponse();
        }

        [HttpPut("cart/{productId}")]
        public async Task<IActionResult> UpdateCartItem(Guid productId, CartItem item)
        {
            var cart = await GetCustomerCart();
            var itemCart = await GetCartItemValid(productId, cart, item);

            if (itemCart == null) return CustomResponse();

            cart.UpdateUnit(itemCart, item.Quantity);

            _context.CartItens.Update(itemCart);
            _context.CustomersCart.Update(cart);

            ValidateCart(cart);
            if (!ValidOperation()) return CustomResponse();

            await PersistData();

            return CustomResponse();
        }

        [HttpDelete("cart/{productId}")]
        public async Task<IActionResult> RemoveCartItem(Guid productId)
        {
            var cart = await GetCustomerCart();

            var itemCart = await GetCartItemValid(productId, cart);
            if (itemCart == null) return CustomResponse();

            ValidateCart(cart);
            if (!ValidOperation()) return CustomResponse();

            cart.RemoveItem(itemCart);

            _context.CartItens.Remove(itemCart);
            _context.CustomersCart.Update(cart);

            await PersistData();
            return CustomResponse();
        }

        private void HandleNewCart(CartItem item)
        {
            var cart = new CustomerCart(_user.GetUserId());

            cart.AddItem(item);

            ValidateCart(cart);
            _context.CustomersCart.Add(cart);
        }

        private void HandleExistingCart(CartItem item, CustomerCart cart)
        {
            var existingProcut = cart.ExistingItemCart(item);

            cart.AddItem(item);
            ValidateCart(cart);
            if (existingProcut)
            {
                _context.Update(cart.GetProductById(item.ProductId));
            }
            else
            {
                _context.Add(item);
            }

            _context.CustomersCart.Update(cart);
        }

        private async Task<CustomerCart> GetCustomerCart()
        {
            return await _context.CustomersCart
                .Include(c => c.Itens)
                .FirstOrDefaultAsync(c => c.ClientId == _user.GetUserId());
        }

        private async Task<CartItem> GetCartItemValid(Guid produtctId, CustomerCart cart, CartItem item = null)
        {
            if (item != null && produtctId != item.ProductId)
            {
                AddProcessingError("O item não corresponde ao informado");
                return null;
            }

            if (cart == null)
            {
                AddProcessingError("Carrinho não encontrado");
                return null;
            }

            var itemCart = await _context.CartItens
                .FirstOrDefaultAsync(c => c.CartId == cart.Id && c.ProductId == produtctId);

            if (itemCart == null || !cart.ExistingItemCart(itemCart))
            {
                AddProcessingError("O Item não está no carrinho");
                return null;
            }

            return itemCart;
        }

        private async Task PersistData()
        {
            var result = await _context.SaveChangesAsync();
            if (result <= 0) AddProcessingError("Não foi possível persistir os dados!");
        }
        private bool ValidateCart(CustomerCart cart)
        {
            if (cart.IsValid()) return true;

            cart.ValidationResult.Errors.ToList().ForEach(e => AddProcessingError(e.ErrorMessage));
            return false;
        }
    }
}
