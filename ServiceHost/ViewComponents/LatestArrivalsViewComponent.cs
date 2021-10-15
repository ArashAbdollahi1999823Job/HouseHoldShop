using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _01_Query.Contracts.Product;
using Microsoft.AspNetCore.Mvc;
using ShopManagement.Infrastructure.EfCore;

namespace ServiceHost.ViewComponents
{
    public class LatestArrivalsViewComponent:ViewComponent
    {

        private readonly IProductQuery _productQuery;
        public LatestArrivalsViewComponent(IProductQuery productQuery)
        {
            _productQuery = productQuery;
        }



        public IViewComponentResult Invoke()
        {
            var products = _productQuery.GetlatestArrivals();
            return View(products);
        }
    }
}
