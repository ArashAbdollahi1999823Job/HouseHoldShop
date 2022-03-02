using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_Query.Contracts.Article;
using _01_Query.Contracts.ArticleCategory;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace _01_Query.Query
{
    public class ArticleCategoryQuery : IArticleCategoryQuery
    {
        private readonly BlogContext _blogContext;

        public ArticleCategoryQuery(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public ArticleCategoryQueryModel GetArticleCategory(string slug)
        {
            var articleCategory = _blogContext
                  .ArticleCategories
                  .Include(x => x.Articles)
                  .Select(x => new ArticleCategoryQueryModel()
                  {
                      Slug = x.Slug,
                      Picture = x.Picture,
                      PictureTitle = x.PictureTitle,
                      PictureAlt = x.PictureAlt,
                      Name = x.Name,
                      Description = x.Description,
                      Keywords = x.Keywords,
                      MetaDescription = x.MetaDescription,
                      CanonicalAddress = x.CanonicalAddress,
                      Articles = MapArticles(x.Articles),
                      ArticlesCount = x.Articles.Count,
                  }).FirstOrDefault(x => x.Slug == slug);

            articleCategory.KeyWordList = articleCategory.Keywords.Split(",").ToList();

            return articleCategory;

        }

        private static List<ArticleQueryModel> MapArticles(List<Article> xArticles)
        {
            return xArticles
                .Select(x => new ArticleQueryModel()
                {
                    Slug = x.Slug,
                    ShortDescription = x.ShortDescription,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    PublishDate = x.PublishDate.ToFarsi(),

                }).ToList();
        }

        public List<ArticleCategoryQueryModel> GetArticleCategories()
        {
            return _blogContext
                .ArticleCategories.
                Include(x => x.Articles).
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
