using Microsoft.Extensions.Options;
using NS.WebApp.MVC.Models;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace NS.WebApp.MVC.Services
{
    public class CartService : Service, ICartService
    {
        private readonly HttpClient _httpClient;

        public CartService(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(appSettings.Value.CartUrl);
        }
        public async Task<CartViewModel> GetCart()
        {
            var response = await _httpClient.GetAsync("/cart/");

            HandleErrorsResponse(response);

            return await DeserializerObjectResponse<CartViewModel>(response);
        }

        public async Task<ResponseResult> AddItemCart(ProductItemViewModel item)
        {
            var product = GetContent(item);

            var response = await _httpClient.PostAsync("/cart/", product);

            if (!HandleErrorsResponse(response)) return await DeserializerObjectResponse<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> UpdateCartItem(Guid productId, ProductItemViewModel item)
        {
            var product = GetContent(item);

            var response = await _httpClient.PutAsync($"/cart/{item.ProductId}", product);

            if (!HandleErrorsResponse(response)) return await DeserializerObjectResponse<ResponseResult>(response);

            return ReturnOk();
        }

        public async Task<ResponseResult> RemoveCartItem(Guid productId)
        {
            var response = await _httpClient.DeleteAsync($"/cart/{productId}");

            if (!HandleErrorsResponse(response)) return await DeserializerObjectResponse<ResponseResult>(response);

            return ReturnOk();
        }
    }
}
