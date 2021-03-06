using System.Collections.Generic;
using _0_Framework.Domain;
using ShopManagement.Application.contracts.ProductCategory;

namespace ShopManagement.Domain.ProductCategoryAgg
{
    public interface  IProductCategoryRepository:IRepository<long,ProductCategory>
    {
        List<ProductCategoryViewModel> GetProductCategories();
        EditProductCategory GetDetailsBy(long id);
        string GetSlugById(long id);
        List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel);
    }
}
