using NS.WebApp.MVC.Models;
using System;
using System.Threading.Tasks;

namespace NS.WebApp.MVC.Services
{
    public interface ICartService
    {
        Task<CartViewModel> GetCart();
        Task<ResponseResult> AddItemCart(ProductItemViewModel item);
        Task<ResponseResult> UpdateCartItem(Guid productId, ProductItemViewModel item);
        Task<ResponseResult> RemoveCartItem(Guid productId);
    }
}
