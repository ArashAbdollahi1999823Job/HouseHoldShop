using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.contracts.Product;
using ShopManagement.Application.contracts.ProductPicture;

namespace ServiceHost.Areas.Administration.Pages.Shop.ProductPicture
{



    public class IndexModel : PageModel
    {
        [TempData] 
        public string message { get; set; }


        public ProductPictureSearchModel SearchModel;

        public List<ProductPictureViewModel> ProductPictures;
        public SelectList Products;


        private readonly IProductPictureApplication _productPictureApplication;
        private readonly IProductApplication _productApplication;



        public IndexModel(IProductPictureApplication productPictureApplication, IProductApplication productApplication)
        {
            _productPictureApplication = productPictureApplication;
            _productApplication = productApplication;
        }

        public void OnGet(ProductPictureSearchModel searchModel)
        {
            Products = new SelectList(_productApplication.GetProducts(), "Id", "Name");
            ProductPictures = _productPictureApplication.Search(searchModel);
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateProductPicture()
            {
                Products = _productApplication.GetProducts()
            };
            return Partial("./Create", command);
        }


        public JsonResult OnPostCreate(CreateProductPicture command)
        {
            var result = _productPictureApplication.Create(command);
            return new JsonResult(result);
        }


        public IActionResult OnGetEdit(long id)
        {
            var product = _productPictureApplication.GetDetails(id);

            product.Products = _productApplication.GetProducts();
            return Partial("./Edit", product);
        }


        public JsonResult OnPostEdit(EditProductPicture command)
        {
            var result = _productPictureApplication.Edit(command);
            return new JsonResult(result);
        }


        public IActionResult OnGetRemove(long id)
        {
            var operation = _productPictureApplication.Remove(id);
            if (operation.IsSuccedded)
            {
                return RedirectToPage("./index");
            }
            else
            {
                return RedirectToPage("./index");
                message = operation.Message;
            }
        }

        public IActionResult OnGetRestore(long id)
        {

            var operation = _productPictureApplication.Restore(id);
            if (operation.IsSuccedded)
            {
                return RedirectToPage("./index");
            }
            else
            {
                return RedirectToPage("./index");
                message = operation.Message;
            }
        }
    }
}
