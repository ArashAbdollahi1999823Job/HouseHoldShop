using System.Collections.Generic;
using _0_Framework.Application;
using BlogManagement.Application.Contract.ArticleCategory;
using BlogManagement.Domain.ArticleCategoryAgg;

namespace BlogManagement.Application
{
   public  class ArticleCategoryApplication:IArticleCategoryApplication
   {
       private readonly IFileUploader _fileUploader;
       private readonly IArticleCategoryRepository _articleCategoryRepository;

       public ArticleCategoryApplication( IFileUploader fileUploader, IArticleCategoryRepository articleCategoryRepository)
       {
           _fileUploader = fileUploader;
           _articleCategoryRepository = articleCategoryRepository;
       }


       public OperationResult Create(CreateArticleCategory command)
       {
           var operation = new OperationResult();

           if (_articleCategoryRepository.Exists(x => x.Name == command.Name))
           {
               return operation.Failed(ApplicationMessages.DuplicatedRecord);
           }

           var slug = command.Slug.Slugify();
           var path = $"{slug}";
           var picturePath = _fileUploader.Upload(command.Picture, path);

            
           var articleCategory = new ArticleCategory(command.Name, command.Description, picturePath, command.ShowOrder, slug,
               command.MetaDescription, command.Keywords, command.CanonicalAddress,command.PictureTitle,command.PictureAlt);


           _articleCategoryRepository.Create(articleCategory);

           _articleCategoryRepository.SaveChanges();

           return operation.IsSuccess();
       }

        public OperationResult Edit(EditArticleCategory command)
        {
            var operation = new OperationResult();

            var articleCategory = _articleCategoryRepository.GetBy(command.Id);

            if (articleCategory==null) return operation.Failed(ApplicationMessages.RecordNotFound);

            if (_articleCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }


            var slug = command.Slug.Slugify();
            var path = $"{slug}";
            var picturePath = _fileUploader.Upload(command.Picture, path);

            articleCategory.Edit(command.Name, command.Description, picturePath, command.ShowOrder, slug,
                command.MetaDescription, command.Keywords, command.CanonicalAddress,command.PictureTitle,command.PictureAlt); 

            _articleCategoryRepository.SaveChanges();

            return operation.IsSuccess();



        }

        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel)
        {
            return _articleCategoryRepository.Search(searchModel);
        }

        public EditArticleCategory GetDetails(long id)
        {
            return _articleCategoryRepository.GetDetails(id);
        }

        public List<ArticleCategoryViewModel> GetArticleCategories()
        {
            return _articleCategoryRepository.GetArticleCategories();
        }
   }
}
