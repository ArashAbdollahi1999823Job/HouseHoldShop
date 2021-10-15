using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.contracts.ProductCategory;

namespace ShopManagement.Application.contracts.Product
{
   public  class CreateProduct
    {
        [Required(ErrorMessage = ApplicationMessages.Required)]
        public string Name { get;  set; }
        [Required(ErrorMessage = ApplicationMessages.Required)]
        public string Code { get;  set; }
        [Required(ErrorMessage = ApplicationMessages.Required)]
        public string ShortDescription { get;  set; }
        [Required(ErrorMessage = ApplicationMessages.Required)]
        public string Description { get;  set; }

        public IFormFile Picture { get;  set; }
        [Required(ErrorMessage = ApplicationMessages.Required)]
        public string PictureAlt { get;  set; }
        [Required(ErrorMessage = ApplicationMessages.Required)]
        public string PictureTitle { get;  set; }
        [Required(ErrorMessage = ApplicationMessages.Required)]
        public string Slug { get;  set; }
        [Required(ErrorMessage = ApplicationMessages.Required)]
        public string Keywords { get;  set; }
        [Required(ErrorMessage = ApplicationMessages.Required)]
        public string MetaDescription { get;  set; }


        [Range(1,100000,ErrorMessage = ApplicationMessages.Required)]
        public long CategoryId { get;  set; }

        public List<ProductCategoryViewModel> Categories { set; get; }

    }
}
