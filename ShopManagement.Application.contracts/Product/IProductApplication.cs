using System.Collections.Generic;   
using _0_Framework.Application;

namespace ShopManagement.Application.contracts.Product
{
    public interface IProductApplication
    {
        public OperationResult Create(CreateProduct command);
        public OperationResult Edit(EditProduct command);
        public List<ProductViewModel> GetProducts();
        public EditProduct GetDetails(long id);
        public List<ProductViewModel> Search(ProductSearchModel searchModel);
    }
}
