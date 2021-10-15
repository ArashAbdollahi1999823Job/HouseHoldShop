using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace ShopManagement.Application.contracts.ProductCategory
{
    public class CreateProductCategory
    {
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Name { get;  set; }
        public string Description { get;  set; }
        [FileExtentionLimitation(new string[]{".jpeg",".jpg",".png"},ErrorMessage = ApplicationMessages.FileFormatInvalid)]
        [MaxFileSize(3*1024*1024,ErrorMessage = ApplicationMessages.MaxFileSize)]
        public IFormFile Picture { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }
        
        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Keywords { get;  set; }

       [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string Slug { get;  set; }

        [Required(ErrorMessage = ValidationMessage.IsRequired)]
        public string MetaDescription { get;  set; }
    }
}
