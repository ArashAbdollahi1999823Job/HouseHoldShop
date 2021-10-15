using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;
using ShopManagement.Application.contracts.Product;

namespace ShopManagement.Application.contracts.ProductPicture
{
   public class CreateProductPicture
    {
        [Range(0,100000,ErrorMessage =ApplicationMessages.Required)]
        public long ProductId { get; set; }
        [MaxFileSize(1*1024*1024,ErrorMessage = ApplicationMessages.MaxFileSize)]
        public IFormFile Picture { get;  set; }
        [Required(ErrorMessage = ApplicationMessages.Required)]
        public string PictureAlt { get;  set; }
        [Required(ErrorMessage = ApplicationMessages.Required)]
        public string PictureTitle { get;  set; }

        public List<ProductViewModel> Products { get; set; }
    }
}
