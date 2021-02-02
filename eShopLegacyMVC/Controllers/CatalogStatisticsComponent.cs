using System.Linq;
using eShopLegacyMVC.Services;
using eShopLegacyMVC.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace eShopLegacyMVC.Controllers
{
    public class CatalogStatisticsComponent : ViewComponent
    {
        private ICatalogService service;

        public CatalogStatisticsComponent(ICatalogService service)
        {
            this.service = service;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = new CatalogStatisticsViewModel
            {
                NumberOfBrands = service.GetCatalogBrands().Count(),
                NumberOfCategories = service.GetCatalogTypes().Count(),
                NumberOfItems = (int)service.GetCatalogItemsPaginated(1000, 0).TotalItems
            };
            return View("~/Views/CatalogStatistics/Index.cshtml", viewModel);
        }
    }
}