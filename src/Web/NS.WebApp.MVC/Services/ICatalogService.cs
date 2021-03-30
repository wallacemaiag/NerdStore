using NS.WebApp.MVC.Models;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NS.WebApp.MVC.Services
{
    public interface ICatalogService
    {
        Task<IEnumerable<ProductViewModel>> GetAll();
        Task<ProductViewModel> GetById(Guid id);
    }

    public interface ICatalogServiceRefit
    {
        [Get("/catalog/products")]
        Task<IEnumerable<ProductViewModel>> GetAll();

        [Get("/catalog/product/{id}")]
        Task<ProductViewModel> GetById(Guid id);
    }
}
