using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Domain;
using ShopManagement.Application.contracts.Product;
using ShopManagement.Application.contracts.ProductPicture;

namespace ShopManagement.Domain.ProductPictureAgg
{
    public interface IProductPictureRepository:IRepository<long,ProductPicture>
    {
        public EditProductPicture GetDetails(long id);

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
    }
}
