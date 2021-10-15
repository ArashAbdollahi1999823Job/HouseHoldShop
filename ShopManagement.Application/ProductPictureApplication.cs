using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Application;
using ShopManagement.Application.contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Application
{
    public class ProductPictureApplication : IProductPictureApplication
    {

        private readonly IProductPictureRepository _productPictureRepository;

        public ProductPictureApplication(IProductPictureRepository productPictureRepository)
        {
            _productPictureRepository = productPictureRepository;
        }





        public OperationResult Create(CreateProductPicture command)
        {
            var operation = new OperationResult();

            if (_productPictureRepository.Exists(
                x => x.Picture == command.Picture &&
                     x.ProductId == command.ProductId
                     ))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            else
            {
                var productPicture = new ProductPicture(
                    command.ProductId, command.Picture
                    , command.PictureAlt, command.PictureTitle);



                _productPictureRepository.Create(productPicture);
                _productPictureRepository.SaveChanges();

                return operation.IsSuccess();
            }
        }

        public OperationResult Edit(EditProductPicture command)
        {
            var operation = new OperationResult();


            var productPicture = _productPictureRepository.GetBy(command.Id);

            if (productPicture == null)
            {
                return operation.Failed(ApplicationMessages.RecordNotFound);
            }
            else if (_productPictureRepository.Exists(
                x =>
                    x.Id != command.Id &&
                   x.Picture == command.Picture &&
                   x.ProductId == command.ProductId))
            {
                return operation.Failed(ApplicationMessages.DuplicatedRecord);
            }
            else
            {
                productPicture.Edit(command.ProductId, command.Picture, command.PictureAlt, command.PictureTitle);

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
