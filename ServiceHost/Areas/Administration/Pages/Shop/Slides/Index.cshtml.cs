using System.Collections.Generic;
using CommentManagement.Application.Contracts.Comment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShopManagement.Application.contracts.Slider;

namespace ServiceHost.Areas.Administration.Pages.Shop.Slides
{



    public class IndexModel : PageModel
    {
        [TempData]
        public string message { get; set; }



        public List<SliderViewModel> Slides;

        private readonly ISliderApplication _sliderApplication;
        public CommentSearchModel SearchModel;




        public IndexModel(ISliderApplication sliderApplication)
        {
            _sliderApplication = sliderApplication;
        }





        public void OnGet()
        {
            Slides = _sliderApplication.GetList();
        }

        public IActionResult OnGetCreate()
        {
            var command = new CreateSlider();
            return Partial("./Create", command);
        }


        public JsonResult OnPostCreate(CreateSlider command)
        {
            var result = _sliderApplication.Create(command);
            return new JsonResult(result);
        }


        public IActionResult OnGetEdit(long id)
        {
            var Slides = _sliderApplication.GetDetails(id);
            return Partial("./Edit", Slides);
        }


        public JsonResult OnPostEdit(EditSlider command)
        {
            var result = _sliderApplication.Edit(command);
            return new JsonResult(result);
        }


        public IActionResult OnGetRemove(long id)
        {
            var operation = _sliderApplication.Remove(id);
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

            var operation = _sliderApplication.Restore(id);
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


