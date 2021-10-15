using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using ShopManagement.Application.contracts.Product;

namespace ShopManagement.Application.contracts.ProductPicture
{
   public class CreateProductPicture
    {
        [Range(0,100000,ErrorMessage =ApplicationMessages.Required)]
        public long ProductId { get; set; }
        public string Picture { get;  set; }
        [Required(ErrorMessage = ApplicationMessages.Required)]
        public string PictureAlt { get;  set; }
        [Required(ErrorMessage = ApplicationMessages.Required)]
        public string PictureTitle { get;  set; }

        public List<ProductViewModel> Products { get; set; }
    }
}
