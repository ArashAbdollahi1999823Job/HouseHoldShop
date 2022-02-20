using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlogManagement.Domain.ArticleAgg;

namespace _01_Query.Contracts.ArticleCategory
{
    public class ArticleCategoryQueryModel
    {
        public string Name { get;  set; }
        public string Description { get;  set; }
        public string Picture { get;  set; }
        public int ShowOrder { get;  set; }
        public string PictureAlt { get;  set; }
        public string PictureTitle { get;  set; }




        public string Slug { get;  set; }
        public string MetaDescription { get;  set; }
        public string Keywords { get;  set; }
        public string CanonicalAddress { get;  set; }
        public int ArticlesCount { get; set; }


       
    }
}
