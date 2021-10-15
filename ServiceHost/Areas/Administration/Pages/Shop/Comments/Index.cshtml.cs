using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ShopManagement.Application.contracts.Comment;
using ShopManagement.Application.contracts.Product;
using ShopManagement.Application.contracts.ProductPicture;
using ShopManagement.Application.contracts.Slider;

namespace ServiceHost.Areas.Administration.Pages.Shop.Comments
{



    public class IndexModel : PageModel
    {
        [TempData]
        public string message { get; set; }

        public CommentViewModel SearchModel;

        public List<CommentViewModel> Comments;

        private readonly ICommentApplication _commentApplication;




        public IndexModel(ICommentApplication commentApplication)
        {
            _commentApplication = commentApplication;
        }





        public void OnGet(CommentSearchModel command)
        {
             Comments = _commentApplication.Search(command);
        }

        public IActionResult OnGetCancel(long id)
        {
            var operation = _commentApplication.Cancel(id);
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

        public IActionResult OnGetConfirm(long id)
        {

            var operation = _commentApplication.Confirm(id);
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