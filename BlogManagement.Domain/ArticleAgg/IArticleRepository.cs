using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using BlogManagement.Application.Contract.Article;

namespace BlogManagement.Domain.ArticleAgg
{
   public interface IArticleRepository:IRepository<long,Article>
   {
       Article GetArticleWithCategory(long id);
       EditArticleModel GetDetails(long id);
       List<ArticleViewModel> Search(ArticleSearchModel searchModel);
   }
}
