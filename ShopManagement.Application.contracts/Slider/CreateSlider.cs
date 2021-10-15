using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using _0_Framework.Application;
using Microsoft.AspNetCore.Http;

namespace ShopManagement.Application.contracts.Slider
{
    public class CreateSlider
    {
        
        public IFormFile Picture { get; set; }
        [Required(ErrorMessage = ApplicationMessages.Required)]
        public string PictureAlt { get; set; }
        [Required(ErrorMessage = ApplicationMessages.Required)]

        public string PictureTitle { get; set; }
        [Required(ErrorMessage = ApplicationMessages.Required)]

        public string Heading { get; set; }
        [Required(ErrorMessage = ApplicationMessages.Required)]

        public string Title { get; set; }
        [Required(ErrorMessage = ApplicationMessages.Required)]

        public string Text { get; set; }
        [Required(ErrorMessage = ApplicationMessages.Required)]

        public string Link { get; set; }
        [Required(ErrorMessage = ApplicationMessages.Required)]

        public string BtnText { get; set; }
    }
}
