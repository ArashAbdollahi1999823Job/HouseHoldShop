 using System;
using System.Collections.Generic;
using ShopManagement.Application.contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;
using _0_Framework.Application;

namespace ShopManagement.Application
{
    public class ProductCategoryApplication : IProductCategoryApplication
    {

        private readonly IFileUploader _fileUploader;
        private readonly IProductCategoryRepository _productCategoryRepository;
        

        
        public ProductCategoryApplication(IProductCategoryRepository productCategoryRepository, IFileUploader fileUploader)
        {
            _productCategoryRepository = productCategoryRepository;
            _fileUploader = fileUploader;
        }






        public OperationResult Create(CreateProductCategory command)
        {
            var operation = new OperationResult();

            if (_productCategoryRepository.Exists(x => x.Name == command.Name))
            {
                return operation.Failed("امکان ثبت کاربر تکراری وجود ندارد");
            }
            else
            {
                var slug = command.Slug.Slugify();
                var path = $"{command.Slug}";
                var picturepath = _fileUploader.Upload(command.Picture, path);
                var productCategory = new ProductCategory
                (command.Name, command.Description
                    ,picturepath, command.PictureAlt
                    , command.PictureTitle, command.Keywords
                    , slug, command.MetaDescription);

                //-------------------------------------
                _productCategoryRepository.Create(productCategory);
                _productCategoryRepository.SaveChanges();
                return operation.IsSuccess();

            }
        }
        public OperationResult Edit(EditProductCategory command)
        {
            var operation = new OperationResult();

            var productCategory = _productCategoryRepository.GetBy(command.Id);

            if (productCategory == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            else if (_productCategoryRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            else
            {
                var slug = command.Slug.Slugify();
                var picturePath = $"{command.Slug}";
                var fileName = _fileUploader.Upload(command.Picture,picturePath);
                productCategory.Edit(
                    command.Name, command.Description,
                    fileName, command.PictureAlt,
                    command.PictureTitle, command.Keywords
                    , slug, command.MetaDescription
                );

                _productCategoryRepository.SaveChanges();
                return operation.IsSuccess();

            }
        }
        public EditProductCategory GetDetails(long id)
        {
            return _productCategoryRepository.GetDetailsBy(id);
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return _productCategoryRepository.GetProductCategories();
        }
        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            return _productCategoryRepository.Search(searchModel);
        }

    }
}
