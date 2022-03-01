using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Query.Contracts.Product
{
    public interface IProductQuery
    {
        ProductQueryModel GetProductDetails(string slug);
        List<ProductQueryModel> GetlatestArrivals();
        List<ProductQueryModel> Search(String value);


    }
}
