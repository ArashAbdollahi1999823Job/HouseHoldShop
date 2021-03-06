using System;

namespace ShopManagement.Application.contracts.Product
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Picture { get; set; }
        public string Code { get; set; }
        public string Category { get; set; }
        public long CategoryId { get; set; }
        public bool IsInStock { get; set; }

        public DateTime CreationDate { get; set; }
    }
}
