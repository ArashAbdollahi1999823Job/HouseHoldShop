using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Query.Contracts.ArticleCategory
{
   public interface IArticleCategoryQuery
   {
       List<ArticleCategoryQueryModel> GetArticleCategories();
   }

}
