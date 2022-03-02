using System.Collections.Generic;
using _01_Query.Contracts.Article;
using _01_Query.Contracts.ArticleCategory;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ServiceHost.Pages
{
    public class ArticleCategoryModel : PageModel
    {
        public ArticleCategoryQueryModel ArticleCategory { get; set; }
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }
        public List<ArticleQueryModel> LatestArticles { get; set; }


        private readonly IArticleQuery _articleQuery;
        private readonly IArticleCategoryQuery _articleCategoryQuery;

        public ArticleCategoryModel(IArticleCategoryQuery articleCategoryQuery, IArticleQuery articleQuery)
        {
            _articleCategoryQuery = articleCategoryQuery;
            _articleQuery = articleQuery;
        }

        public void OnGet(string slug)
        {

            ArticleCategory = _articleCategoryQuery.GetArticleCategory(slug);
            ArticleCategories = _articleCategoryQuery.GetArticleCategories();

            LatestArticles = _articleQuery.LatestArticles();
        }
    }
}
