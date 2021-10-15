using System;
using System.Collections.Generic;
using System.Text;
using _0_Framework.Application;

namespace ShopManagement.Application.contracts.ProductPicture
{
   public interface IProductPictureApplication
   {
       public OperationResult Create(CreateProductPicture command);
       public OperationResult Edit(EditProductPicture command);
       public OperationResult Remove(long id);
       public OperationResult Restore(long id);
       public EditProductPicture GetDetails(long id);
       public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel);
   }
}
