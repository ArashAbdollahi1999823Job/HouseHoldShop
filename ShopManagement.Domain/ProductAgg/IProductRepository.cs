using System.Collections.Generic;
using _0_Framework.Domain;
using ShopManagement.Application.contracts.Product;

namespace ShopManagement.Domain.ProductAgg
{
    public interface IProductRepository:IRepository<long,Product>
    {
        Product GetProductWhitCategory(long id);
        EditProduct GetDetails(long id);

        List<ProductViewModel> GetProducts();
        List<ProductViewModel> Search(ProductSearchModel searchModel);

    }
}
