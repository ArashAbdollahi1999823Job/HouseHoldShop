using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using BlogManagement.Domain.ArticleAgg;

namespace BlogManagement.Domain.ArticleCategoryAgg
{
     public class ArticleCategory:EntityBase
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Picture { get; private set; }
        public int ShowOrder { get; private set; }
        public string PictureAlt { get; private set; }
        public string PictureTitle { get; private set; }




        public string Slug { get; private set; }
        public string MetaDescription { get; private set; }
        public string Keywords { get; private set; }
        public string CanonicalAddress { get; private set; }


        public List<Article> Articles { get;private set; }


        public ArticleCategory(string name, string description, string picture, int showOrder, 
            string slug, string metaDescription, string keywords, string canonicalAddress, string pictureTitle, string pictureAlt)
        {
            Name = name;
            Description = description;
            Picture = picture;
            ShowOrder = showOrder;
            Slug = slug;
            MetaDescription = metaDescription;
            Keywords = keywords;
            CanonicalAddress = canonicalAddress;
            PictureTitle = pictureTitle;
            PictureAlt = pictureAlt;
        }


        public void Edit(string name, string description, string picture, int showOrder,
            string slug, string metaDescription, string keywords, string canonicalAddress, string pictureTitle, string pictureAlt)
        {
            Name = name;
            Description = description;
            if(!string.IsNullOrWhiteSpace(picture)) Picture = picture;
            ShowOrder = showOrder;
            Slug = slug;
            MetaDescription = metaDescription;
            Keywords = keywords;
            CanonicalAddress = canonicalAddress;
            PictureTitle = pictureTitle;
            PictureAlt = pictureAlt;
        }
    }
}
