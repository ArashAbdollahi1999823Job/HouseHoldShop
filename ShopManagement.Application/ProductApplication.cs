using System.Collections.Generic;
using _0_Framework.Application;
using ShopManagement.Application.contracts.Product;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Application
{
    public class ProductApplication : IProductApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly IProductRepository _productRepository;
        private readonly IProductCategoryRepository _productCategoryRepository;

        public ProductApplication(IProductRepository productRepository, IFileUploader fileUploader, IProductCategoryRepository productCategoryRepository)
        {
            _productRepository = productRepository;
            _fileUploader = fileUploader;
            _productCategoryRepository = productCategoryRepository;
        }


        public OperationResult Create(CreateProduct command)
        {
            var operation = new OperationResult();

            if (_productRepository.Exists(x => x.Name == command.Name))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            else
            {


                var slug = command.Slug.Slugify();
                var categorySlug = _productCategoryRepository.GetSlugById(command.CategoryId);
                var path = $"{categorySlug}//{command.Slug}";
                var picturePath = _fileUploader.Upload(command.Picture,path);
                var product = new Product(
                    command.Name, command.Code
                    , command.ShortDescription, command.Description,
                    picturePath, command.PictureAlt, command.PictureTitle
                    , slug, command.Keywords, command.MetaDescription
                    , command.CategoryId);

                _productRepository.Create(product);
                _productRepository.SaveChanges();
                return operation.IsSuccess();
            }

        }

        public OperationResult Edit(EditProduct command)
        {
            var operation = new OperationResult();
            var product = _productRepository.GetProductWhitCategory(command.Id);

            if (product == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            else if (_productRepository.Exists(x => x.Name == command.Name && x.Id != command.Id))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            else
            {
                var slug = command.Slug.Slugify();
                var path = $"{product.Category.Slug}/{command.Slug}";
                var picturePath = _fileUploader.Upload(command.Picture, path);
                product.Edit(
                    command.Name, command.Code
                    , command.ShortDescription, command.Description,
                    picturePath, command.PictureAlt, command.PictureTitle
                    , slug, command.Keywords, command.MetaDescription
                    , command.CategoryId);

                _productRepository.SaveChanges();
                ;
                return operation.IsSuccess();
            }
        }

        public List<ProductViewModel> GetProducts()
        {
            return _productRepository.GetProducts();
        }

        public EditProduct GetDetails(long id)
        {
            return _productRepository.GetDetails(id);
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            return _productRepository.Search(searchModel);
        }
    }
}
