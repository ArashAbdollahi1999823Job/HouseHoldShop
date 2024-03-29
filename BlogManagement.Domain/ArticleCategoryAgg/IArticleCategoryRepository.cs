﻿using System.Collections.Generic;
using _0_Framework.Domain;
using BlogManagement.Application.Contract.ArticleCategory;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository:IRepository<long,ArticleCategory>
    {

        string GetSlugById(long id);
        EditArticleCategory GetDetails(long id);
        List<ArticleCategoryViewModel> GetArticleCategories();

        List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
    }
}
