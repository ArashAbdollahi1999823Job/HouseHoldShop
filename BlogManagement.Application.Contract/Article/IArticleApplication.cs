using System.Collections.Generic;
using _0_Framework.Application;

namespace BlogManagement.Application.Contract.Article
{
    public  interface IArticleApplication
    {
        OperationResult Create(CreateArticleModel command);
        OperationResult Edit(EditArticleModel command);
        EditArticleModel GetDetails(long id);
        List<ArticleViewModel> Search(ArticleSearchModel searchModel);
    }
}
