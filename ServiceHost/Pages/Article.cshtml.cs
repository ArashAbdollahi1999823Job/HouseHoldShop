using System.Collections.Generic;
using _01_Query.Contracts.Article;
using _01_Query.Contracts.ArticleCategory;
using CommentManagement.Application.Contracts.Comment;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {
        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;
        private readonly ICommentApplication _commentApplication;

        public ArticleQueryModel Article { get; set; }
        public List<ArticleQueryModel> LatestArticles { get; set; }
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }


        public ArticleModel(IArticleQuery articleQuery, IArticleCategoryQuery articleCategoryQuery, ICommentApplication commentApplication)
        {
            _articleQuery = articleQuery;
            _articleCategoryQuery = articleCategoryQuery;
            _commentApplication = commentApplication;
        }


        public void OnGet(string slug)
        {

            Article=_articleQuery.GetArticleDetails(slug);
            LatestArticles = _articleQuery.LatestArticles();
            ArticleCategories = _articleCategoryQuery.GetArticleCategories();

        }


        public IActionResult OnPost(AddComment command, string articleSlug)
        {
            command.Type = CommentType.Article;
            var result = _commentApplication.Add(command);

            return RedirectToPage("/Article", new { slug = articleSlug });
        }
    }
}
