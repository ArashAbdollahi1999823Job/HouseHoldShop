using BlogManagement.Application.Contract.Article;
using BlogManagement.Application.Contract.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ServiceHost.Areas.Administration.Pages.Blog.ArticleCategories;

namespace ServiceHost.Areas.Administration.Pages.Blog.Articles
{
    public class CreateModel : PageModel
    {

        public SelectList ArticleCategories;
        public CreateArticleModel Command;
        private readonly IArticleCategoryApplication _articleCategoryApplication;
        private readonly IArticleApplication _articleApplication;

        public CreateModel(IArticleCategoryApplication articleCategoryApplication, IArticleApplication articleApplication)
        {
            _articleCategoryApplication = articleCategoryApplication;
            _articleApplication = articleApplication;
        }
        public void OnGet()
        {
            ArticleCategories =new SelectList(_articleCategoryApplication.GetArticleCategories(),"Id","Name");
        }

        public IActionResult OnPost(CreateArticleModel command)
        {
           var result= _articleApplication.Create(command);
            return RedirectToPage("./Index");
        }
    }
}
