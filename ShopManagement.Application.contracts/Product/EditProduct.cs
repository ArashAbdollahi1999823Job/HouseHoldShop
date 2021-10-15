using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace ShopManagement.Application.contracts.Product
{
    public class EditProduct:CreateProduct
    {
        public long Id { set; get; }
    }
}
