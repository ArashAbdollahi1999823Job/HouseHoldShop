using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _01_Query.Contracts.ArticleCategory;
using _01_Query.Contracts.ProductCategory;

namespace _01_Query
{
    public class MenuModel
    {
        public List<ProductCategoryQueryModel> ProductCategories { set; get; }
        public List<ArticleCategoryQueryModel> ArticleCategories { get; set; }

    }
}
