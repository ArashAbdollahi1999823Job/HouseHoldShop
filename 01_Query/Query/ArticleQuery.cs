using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_Query.Contracts.Article;
using _01_Query.Contracts.Comment;
using BlogManagement.Infrastructure.EFCore;
using CommentManagement.Infrastructure.EFCore;
using Microsoft.EntityFrameworkCore;

namespace _01_Query.Query
{
    public class ArticleQuery : IArticleQuery
    {
        private readonly BlogContext _context;
        private readonly CommentContext _commentContext;

        public ArticleQuery(BlogContext context, CommentContext commentContext)
        {
            _context = context;
            _commentContext = commentContext;
        }

        public ArticleQueryModel GetArticleDetails(string slug)
        {
            var article = _context
                 .Articles
                 .Include(x => x.Category)
                 .Where(x => x.PublishDate <= DateTime.Now)
                 .Select(x => new ArticleQueryModel()
                 {
                     Id = x.Id,
                    //  CategoryId = x.CategoryId,
                    CategoryName = x.Category.Name,
                     CategorySlug = x.Category.Slug,

                     Slug = x.Slug,
                     CanonicalAddress = x.CanonicalAddress,
                     Description = x.Description,
                     Keywords = x.Keywords,
                     MetaDescription = x.MetaDescription,


                     Picture = x.Picture,
                     PictureAlt = x.PictureAlt,
                     PictureTitle = x.PictureTitle,

                     PublishDate = x.PublishDate.ToFarsi(),
                     ShortDescription = x.ShortDescription,
                     Title = x.Title,
                 }).FirstOrDefault(x => x.Slug == slug);

            /*var comments =
                _commentContext
                    .Comments
                    /*.Where(x => !x.IsCanceled)
                    .Where(x => x.IsConfirmed)
                    .Where(x => x.Type == CommentType.Article)
                    .Where(x => x.OwnerRecordId == article.Id)#1#
                    .Select(x => new CommentQueryModel()
                    {
                        Id = x.Id,
                        Message = x.Message,
                        Name = x.Name,
                        CreationDate = x.CreationDate.ToFarsi(),
                        ParentId = x.Parent.Id,
                        ParentName = x.Parent.Name
                        
                    })
                    .OrderByDescending(x => x.Id)
                    .ToList();*/

            var comments
                = _commentContext
                    .Comments
                    .Where(x => !x.IsCanceled)
                    .Where(x => x.IsConfirmed)
                    .Where(x => x.Type == CommentType.Article)
                    .Where(x => x.OwnerRecordId == article.Id)
                    .Select(x => new CommentQueryModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        ParentId = x.ParentId,
                        Message = x.Message,
                        CreationDate = x.CreationDate.ToFarsi(),
                    })
                    .OrderByDescending(x=>x.Id)
                .ToList();


            foreach (var comment in comments)
            {
                if (comment.ParentId > 0)
                {
                    comment.ParentName = comments.FirstOrDefault(x => x.Id == comment.ParentId)?.Name;
                }
            }

            article.Comments = comments;

            article.KeyWordList = article.Keywords.Split(",").ToList();
                return article;
        }

        public List<ArticleQueryModel> LatestArticles()
        {
            return _context
                .Articles
                .Include(x => x.Category)
                .Where(x => x.PublishDate <= DateTime.Now)
                .Select(x => new ArticleQueryModel()
                {
                    /*      CategoryId = x.CategoryId,
                          CategoryName = x.Category.Name,
                          CategorySlug = x.Category.Slug,*/

                    Slug = x.Slug,
                    /*      CanonicalAddress = x.CanonicalAddress,
                          Description = x.Description,
                          Keywords = x.Keywords,
                          MetaDescription = x.MetaDescription,*/


                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,

                    PublishDate = x.PublishDate.ToFarsi(),
                    ShortDescription = x.ShortDescription,
                    Title = x.Title,
                }).ToList();
        }
    }
}
