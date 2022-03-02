using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace BlogManagement.Application.Contract.ArticleCategory
{
    public class CreateArticleCategory
    {
        [Required(ErrorMessage = ApplicationMessages.Required)]
        public string Name { get; set; }
        [Required(ErrorMessage = ApplicationMessages.Required)]
        public string Description { get; set; }
        public IFormFile Picture { get; set; }
        [Required(ErrorMessage = ApplicationMessages.Required)]
        public int ShowOrder { get; set; }

        [Required(ErrorMessage = ApplicationMessages.Required)]

        public string PictureAlt { get;  set; }
        [Required(ErrorMessage = ApplicationMessages.Required)]

        public string PictureTitle { get;  set; }


        [Required(ErrorMessage = ApplicationMessages.Required)]
        public string Slug { get; set; }
        public string MetaDescription { get; set; }
        [Required(ErrorMessage = ApplicationMessages.Required)]
        public string Keywords { get; set; }
        public string CanonicalAddress { get; set; }

    }
}
