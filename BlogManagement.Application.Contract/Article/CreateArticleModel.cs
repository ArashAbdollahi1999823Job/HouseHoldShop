using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace BlogManagement.Application.Contract.Article
{
    public class CreateArticleModel
    {
        [Required(ErrorMessage = ApplicationMessages.Required)]

        public string Title { get; set; }
        [Required(ErrorMessage = ApplicationMessages.Required)]

        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public IFormFile Picture { get; set; }
        [Required(ErrorMessage = ApplicationMessages.Required)]
        public string PublishDate { get; set; }



        [Required(ErrorMessage = ApplicationMessages.Required)]
        public string PictureTitle { get; set; }
        [Required(ErrorMessage = ApplicationMessages.Required)]
        public string PictureAlt { get; set; }
        [Required(ErrorMessage = ApplicationMessages.Required)] 
        public string Keywords { get; set; }
        [Required(ErrorMessage = ApplicationMessages.Required)]
        public string Slug { get; set; }
        [Required(ErrorMessage = ApplicationMessages.Required)]
        public string MetaDescription { get; set; }
        public string CanonicalAddress { get; set; }

        [Range(1,long.MaxValue,ErrorMessage = ApplicationMessages.Required)]
        public long CategoryId { get; set; }
    }
}
