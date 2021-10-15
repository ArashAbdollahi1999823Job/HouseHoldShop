using _0_Framework.Domain;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Domain.ArticleAgg
{
    public class Article : EntityBase
    {
        public string Title { get; set; }
        public long CategoryId { get; set; }
        public ArticleCategory ArticleCategory { get; set; }
    }
}
