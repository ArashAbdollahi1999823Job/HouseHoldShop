using System.Collections.Generic;
using _0_Framework.Application;
using ShopManagement.Application.contracts.ProductPicture;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {

        private readonly IProductPictureRepository _productPictureRepository;
        private readonly IProductRepository _productRepository;
        private readonly IFileUploader _fileUploader;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository, IProductRepository productRepository, IFileUploader fileUploader)
        {
            _productPictureRepository = productPictureRepository;
            _productRepository = productRepository;
            _fileUploader = fileUploader;
        }





        public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();

        /*    if (_productPictureRepository.Exists(
                x => x.Picture == command.Picture &&
                     x.ProductId == command.ProductId
                     ))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            else*/
           // {
           var product = _productRepository.GetProductWhitCategory(command.ProductId);
           var path = $"{product.Category.Slug}//{product.Slug}";
           var picturePath = _fileUploader.Upload(command.Picture, path);
                var productPicture = new ProductPicture(
                    command.ProductId, picturePath
                    , command.PictureAlt, command.PictureTitle);



                _productPictureRepository.Create(productPicture);
                _productPictureRepository.SaveChanges();

                return operation.IsSuccess();
          //  }
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();


            var productPicture = _productPictureRepository.GetWithProductAndCategory(command.Id);

            if (productPicture == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
       /*     else if (_productPictureRepository.Exists(
                x =>
                    x.Id != command.Id &&
                   x.Picture == command.Picture &&
                   x.ProductId == command.ProductId))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }*/
            else
            {
                var path = $"{productPicture.Product.Category.Slug}//{productPicture.Product.Slug}";
                var picturePath = _fileUploader.Upload(command.Picture, path);
                productPicture.Edit(command.ProductId, picturePath, command.PictureAlt, command.PictureTitle);

                _productPictureRepository.SaveChanges();
                return operation.IsSuccess();

            }

        }

        public EditProductPicture GetDetails(long id)
        {
            return _productPictureRepository.GetDetails(id);
        }

        public OperationResult Remove(long id)
        {
            var operation=new OperationResult();
            var productPicture= _productPictureRepository.GetBy(id);
            if (productPicture == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            else
            {
                productPicture.Remove();
                _productPictureRepository.SaveChanges();
                return operation.IsSuccess();
            }
        }

        public OperationResult Restore(long id)
        {

            var operation = new OperationResult();
            var productPicture = _productPictureRepository.GetBy(id);
            if (productPicture == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            else
            {
                productPicture.Restore();
                _productPictureRepository.SaveChanges();
                return operation.IsSuccess();
            }
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            return _productPictureRepository.Search(searchModel);
        }
    }
}
