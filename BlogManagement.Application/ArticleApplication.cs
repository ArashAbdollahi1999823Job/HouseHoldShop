﻿using System.Collections.Generic;
using _0_Framework.Application;
using BlogManagement.Application.Contract.Article;
using BlogManagement.Domain.ArticleAgg;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
    public class ArticleApplication :IArticleApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IArticleRepository _articleRepository;
        private readonly IArticleCategoryRepository _articleCategoryRepository;


        public ArticleApplication(IFileUploader fileUploader, IArticleRepository articleRepository, IArticleCategoryRepository articleCategoryRepository)
        {
            _fileUploader = fileUploader;
            _articleRepository = articleRepository;
            _articleCategoryRepository = articleCategoryRepository;
        }

        public OperationResult Create(CreateArticleModel command)
        {
            var operation = new OperationResult();

            if (_articleRepository.Exists(x => x.Title == command.Title))
                return operation.Failed(ApplicationMessages.DuplicatedRecord);

          

            var slug = command.Slug.Slugify();
            var categorySlug =_articleCategoryRepository.GetSlugById(command.CategoryId);
            var path =$"{categorySlug}/{slug}";

            var pictureName= _fileUploader.Upload(command.Picture, path);

            var publishDate = command.PublishDate.ToGeorgianDateTime();

            var article = new Article(command.Title, command.ShortDescription, command.Description, pictureName, publishDate
                , command.PictureTitle, command.PictureAlt, command.Keywords, command.Slug, command.MetaDescription, command.CanonicalAddress
                , command.CategoryId);

            _articleRepository.Create(article);

            _articleRepository.SaveChanges();
            return operation.IsSuccess();

        }

        public OperationResult Edit(EditArticleModel command)
        {
            var operationResult = new OperationResult();

            var article = _articleRepository.GetArticleWithCategory( command.Id);

            if (article == null) return operationResult.Failed(ApplicationMessages.RecordNotFound);

            if (_articleRepository.Exists(x => x.Title == command.Title && x.Id !=command.Id))
                return operationResult.Failed(ApplicationMessages.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var path = $"{article.Category.Slug}/{slug}";

            var pictureName = _fileUploader.Upload(command.Picture, path);

            var publishDate = command.PublishDate.ToGeorgianDateTime();

            article.Edit(command.Title, command.ShortDescription, command.Description, pictureName, publishDate
                , command.PictureTitle, command.PictureAlt, command.Keywords, command.Slug, command.MetaDescription, command.CanonicalAddress
                , command.CategoryId);


            _articleRepository.SaveChanges();
            return operationResult.IsSuccess();
        }

        public EditArticleModel GetDetails(long id)
        {
            return _articleRepository.GetDetails(id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            return _articleRepository.Search(searchModel);
        }
    }
}
