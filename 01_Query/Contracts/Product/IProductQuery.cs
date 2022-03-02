using System;
using System.Collections.Generic;

namespace _01_Query.Contracts.Product
{
    public interface IProductQuery
    {
        ProductQueryModel GetProductDetails(string slug);
        List<ProductQueryModel> GetlatestArrivals();
        List<ProductQueryModel> Search(String value);


    }
}
