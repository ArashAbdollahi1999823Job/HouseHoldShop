using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using _0_Framework.Application;
using ShopManagement.Application.contracts.Product;

namespace InventoryManagement.Application.Contract.Inventory
{
    public class CreateInventory
    {
        [Range(1,1000000,ErrorMessage = ApplicationMessages.Required)]
        public long ProductId { get; set; }
        [Range(1, double.MaxValue, ErrorMessage = ApplicationMessages.Required)]
        public double UnitPrice { get; set; }
        public List<ProductViewModel> Products { set; get; }
    };

}