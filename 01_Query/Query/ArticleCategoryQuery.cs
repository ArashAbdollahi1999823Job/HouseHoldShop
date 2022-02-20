using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Query.Contracts.Article;
using _01_Query.Contracts.ArticleCategory;
using BlogManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace _01_Query.Query
{
    public class ArticleCategoryQuery:IArticleCategoryQuery
    {
        private readonly BlogContext _blogContext;

        public ArticleCategoryQuery(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public List<ArticleCategoryQueryModel> GetArticleCategories()
        {
            return _blogContext
                .ArticleCategories.
                Include(x=>x.Articles).
                Select(x => new ArticleCategoryQueryModel()
            {
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    Slug = x.Slug,
                    ArticlesCount = x.Articles.Count,

            }).ToList();
        }
    }
}
