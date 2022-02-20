using System.Collections.Generic;
using _01_Query.Contracts.Article;
using _01_Query.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleModel : PageModel
    {
        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;

        public ArticleQueryModel Article { get; set; }
        public List<ArticleQueryModel> LatestArticles { get; set; }
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }


        public ArticleModel(IArticleQuery articleQuery, IArticleCategoryQuery articleCategoryQuery)
        {
            _articleQuery = articleQuery;
            _articleCategoryQuery = articleCategoryQuery;
        }


        public void OnGet(string slug)
        {

            Article=_articleQuery.GetArticleDetails(slug);
            LatestArticles = _articleQuery.LatestArticles();
            ArticleCategories = _articleCategoryQuery.GetArticleCategories();

        }
    }
}
