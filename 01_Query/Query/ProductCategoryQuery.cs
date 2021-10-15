using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_Query.Contracts.Product;
using _01_Query.Contracts.ProductCategory;
using DiscountManagement.infrastructure.EFCore;
using InventoryManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Infrastructure.EfCore;

namespace _01_Query.Query
{
    public class ProductCategoryQuery : IProductCategoryQuery
    {
        private readonly ShopContext _context;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;

        public ProductCategoryQuery(ShopContext context, InventoryContext inventoryContext, DiscountContext discountContext)
        {
            _context = context;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
        }

        public ProductCategoryQueryModel GetCategoryWithProductsBy(string slug)
        {
            var inventory = _inventoryContext
                .Inventory
                .Select(x => new { x.ProductId, x.UnitPrice }).ToList();

            var discounts = _discountContext
                .CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate,x.EndDate })
                .ToList();

            var category = _context
                .ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Products = MapProducts(x.Products),
                    Description=x.Description,
                    MetaDescription = x.MetaDescription,
                    Keywords = x.Keywords,
                    Slug = x.Slug,
                })
                .FirstOrDefault(x=>x.Slug==slug);


                foreach (var product in category.Products)
                {
                    var inventoryPrice = inventory
                        .FirstOrDefault(x => x.ProductId == product.Id);
                    if (inventoryPrice != null)
                    {

                        var price = inventoryPrice.UnitPrice;
                        product.Price = price.ToMoney();


                        var discount = discounts
                            .FirstOrDefault(x => x.ProductId == product.Id);
                        if (discount != null)
                        {
                            var discountRate = discount.DiscountRate;

                            product.DiscountRate = discountRate;

                            product.HasDiscount = discountRate > 0;

                            var discountAmount = Math.Round((price * discountRate) / 100);

                            product.PriceWithDiscount = (price - discountAmount).ToMoney();
                            product.DiscountExpireDate = discount.EndDate.ToDiscountFormat ();
                        }
                    }
                }
            return category;
        }

        public List<ProductCategoryQueryModel> GetProductCategory()
        {
            return _context.ProductCategories.Select(x => new ProductCategoryQueryModel()
            {
                Picture = x.Picture,
                Name = x.Name,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
                Id = x.Id,
            }).ToList();
        }


        public List<ProductCategoryQueryModel> GetProductCategoriesWithProducts()
        {
            var inventory = _inventoryContext
                .Inventory
                .Select(x => new { x.ProductId, x.UnitPrice }).ToList();

            var discounts = _discountContext
                .CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate })
                .ToList();

            var categories = _context
                .ProductCategories
                .Include(x => x.Products)
                .ThenInclude(x => x.Category)
                .Select(x => new ProductCategoryQueryModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Products = MapProducts(x.Products)
                })
                .ToList();


            foreach (var category in categories)
            {
                foreach (var product in category.Products)
                {
                    var inventoryPrice = inventory
                        .FirstOrDefault(x => x.ProductId == product.Id);
                    if (inventoryPrice != null)
                    {

                        var price = inventoryPrice.UnitPrice;
                            product.Price = price.ToMoney();


                            var discount = discounts
                                .FirstOrDefault(x => x.ProductId == product.Id);
                            if (discount != null)
                            {
                                var discountRate = discount.DiscountRate;

                                product.DiscountRate = discountRate;

                                product.HasDiscount = discountRate > 0;

                                var discountAmount = Math.Round((price * discountRate) / 100);

                                product.PriceWithDiscount = (price - discountAmount).ToMoney();
                            }
                    }
                }
            }
            return categories;
        }

        public static List<ProductQueryModel> MapProducts(List<Product> products)
        {
            return products
                .Select(x => new ProductQueryModel()
                {
                    Id = x.Id,
                    Category = x.Category.Name,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                }).ToList();

        }
    }
}
