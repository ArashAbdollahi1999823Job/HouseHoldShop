using System.Collections.Generic;

namespace _01_Query.Contracts.ProductCategory
{
    public interface IProductCategoryQuery
    {

        ProductCategoryQueryModel GetCategoryWithProductsBy(string slug);
        List<ProductCategoryQueryModel> GetProductCategory();
        List<ProductCategoryQueryModel> GetProductCategoriesWithProducts();
    }
}