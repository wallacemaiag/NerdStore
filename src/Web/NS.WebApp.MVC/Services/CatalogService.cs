using Microsoft.Extensions.Options;
using NS.WebApp.MVC.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NS.WebApp.MVC.Services
{
    public class CatalogService : Service, ICatalogService
    {
        private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient, IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.CatalogUrl);

            _httpClient = httpClient;
        }

        public async Task<IEnumerable<ProductViewModel>> GetAll()
        {
            var response = await _httpClient.GetAsync("/catalog/products/");

            HandleErrorsResponse(response);

            return await DeserializerObjectResponse<IEnumerable<ProductViewModel>>(response);
        }

        public async Task<ProductViewModel> GetById(Guid id)
        {
            var response = await _httpClient.GetAsync($"/catalog/product/{id}");

            HandleErrorsResponse(response);

            return await DeserializerObjectResponse<ProductViewModel>(response);
        }
    }
}
