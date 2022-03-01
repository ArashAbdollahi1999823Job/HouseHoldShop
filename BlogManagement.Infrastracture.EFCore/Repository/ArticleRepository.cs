using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using BlogManagement.Application.Contract.Article;
using BlogManagement.Domain.ArticleAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace BlogManagement.Infrastructure.EFCore.Repository
{
    public  class ArticleRepository:RepositoryBase<long,Article>,IArticleRepository
    {


        private readonly BlogContext _context;


        public ArticleRepository(BlogContext context) : base(context)
        {
            _context = context;
        }

        public Article GetArticleWithCategory(long id)
        {
            return _context.Articles.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
        }

        public EditArticleModel GetDetails(long id)
        {
            return _context
                .Articles
                .Select(x => new EditArticleModel
                {
                    CategoryId = x.CategoryId,
                    CanonicalAddress = x.CanonicalAddress,
                    Description = x.Description,
                    Id = x.Id,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                  //  Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    PublishDate = x.PublishDate.ToFarsi(),
                    ShortDescription = x.ShortDescription,
                    Slug = x.Slug,
                    Title = x.Title,
                })
                .FirstOrDefault(x => x.Id == id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {

            var query = _context.Articles
                .Select(x => new ArticleViewModel
                {
                    Id = x.Id,
                    CategoryId = x.CategoryId,
                    Picture = x.Picture,
                    PublishDate = x.PublishDate.ToFarsi(),
                    ShortDescription = x.ShortDescription.Substring(0,Math.Min(x.ShortDescription.Length,50))+"...",
                    Title = x.Title,
                    Category = x.Category.Name,
                });

            if(!string.IsNullOrWhiteSpace(searchModel.Title))query = query.Where(x => x.Title.Contains( searchModel.Title));

            if(searchModel.CategoryId > 0) query = query.Where(x => x.CategoryId == searchModel.CategoryId);


            return query.OrderByDescending(x => x.Id).ToList();

        }
    }
}
 