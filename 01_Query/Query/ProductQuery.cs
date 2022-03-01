using System;
using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _01_Query.Contracts.Comment;
using _01_Query.Contracts.Product;
using CommentManagement.Infrastructure.EFCore;
using DiscountManagement.infrastructure.EFCore;
using InventoryManagement.Infrastructure.EfCore;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Infrastructure.EfCore;

namespace _01_Query.Query
{
    public class ProductQuery : IProductQuery
    {

        private readonly ShopContext _context;
        private readonly InventoryContext _inventoryContext;
        private readonly DiscountContext _discountContext;
        private readonly CommentContext _commentContext;

        public ProductQuery(ShopContext context, InventoryContext inventoryContext, DiscountContext discountContext, CommentContext commentContext)
        {
            _context = context;
            _inventoryContext = inventoryContext;
            _discountContext = discountContext;
            _commentContext = commentContext;
        }

        public ProductQueryModel GetProductDetails(string slug)
        {
            var inventory =
                _inventoryContext
                    .Inventory
                    .Select(x => new { x.ProductId, x.UnitPrice, x.InStock })
                    .ToList();

            var discounts =
                _discountContext
                    .CustomerDiscounts
                    .Select(x => new { x.DiscountRate, x.ProductId, x.StartDate, x.EndDate })
                    .Where(x => x.EndDate > DateTime.Now && x.StartDate < DateTime.Now)
                    .ToList();



            var product =
                _context
                    .Products
                    .Include(x => x.Category)
                    .Include(x => x.ProductPictures)
                    .Select(x => new ProductQueryModel()
                    {
                        Id = x.Id,
                        Category = x.Category.Name,
                        Name = x.Name,
                        Picture = x.Picture,
                        PictureAlt = x.PictureAlt,
                        PictureTitle = x.PictureTitle,
                        Slug = x.Slug,
                        CategorySlug = x.Category.Slug,
                        ShortDescription = x.ShortDescription,
                        Code = x.Code,
                        Description = x.Description,
                        MetaDescription = x.MetaDescription,
                        Keywords = x.Keywords,
                        Pictures = MapProductPicture(x.ProductPictures),
                    })
                    .AsNoTracking()
                    .FirstOrDefault(x => x.Slug == slug);

            product.Comments =
                _commentContext
                    .Comments
                    .Where(x => x.Type == CommentType.Product)
                    .Where(x => x.OwnerRecordId == product.Id)
                    .Where(x => x.IsConfirmed)
                    .Where(x => !x.IsCanceled)
                    .Select(x => new CommentQueryModel()
                    {
                        Id = x.Id,
                        Message = x.Message,
                        Name = x.Name,
                        CreationDate = x.CreationDate.ToFarsi(),
                    })
                    .OrderByDescending(x => x.Id)
                    .ToList();

            var inventoryePrice =
                inventory
                    .FirstOrDefault(x => x.ProductId == product.Id);

            if (inventoryePrice != null)
            {
                product.IsInStock = inventoryePrice.InStock;
                var price = inventoryePrice.UnitPrice;
                product.Price = price.ToMoney();





                var discountRate = discounts
                    .FirstOrDefault(x => x.ProductId == product.Id);

                if (discountRate != null)
                {
                    var discount = discountRate.DiscountRate;
                    product.DiscountRate = discount;

                    product.HasDiscount = discount > 0;


                    var priceWithDiscount = price - (price * discount) / 100;


                    product.PriceWithDiscount = priceWithDiscount.ToMoney();
                    product.DiscountExpireDate = discountRate.EndDate.ToDiscountFormat();
                }
            }





            return product;
        }

        /*
        private static List<CommentQueryModel> MapComments(List<Comment> comments)
        {
            return comments
                .Where(x => x.IsConfirmed)
                .Where(x=>!x.IsCanceled)
                .Select(x => new CommentQueryModel
            {
                Id = x.Id,
                Message = x.Message,
                Name = x.Name,
            }).OrderByDescending(x=>x.Id).ToList();
        }
        */

        private static List<ProductPictureQueryModel> MapProductPicture(List<ProductPicture> pictures)
        {
            return pictures
                .Select(x => new ProductPictureQueryModel
                {
                    IsRemoved = x.IsRemoved,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    ProductId = x.ProductId,

                }).Where(x => !x.IsRemoved)
                .ToList();
        }

        public List<ProductQueryModel> GetlatestArrivals()
        {

            var inventory = _inventoryContext
                .Inventory
                .Select(x => new { x.ProductId, x.UnitPrice }).ToList();

            var discounts = _discountContext
                .CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate })
                .ToList();

            var products = _context.Products.Select(x => new ProductQueryModel()
            {
                Id = x.Id,
                Category = x.Category.Name,
                Name = x.Name,
                Picture = x.Picture,
                PictureAlt = x.PictureAlt,
                PictureTitle = x.PictureTitle,
                Slug = x.Slug,
            })
                .OrderByDescending(x => x.Id)
                .Take(6)
                .ToList();

            foreach (var product in products)
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

            return products;
        }

        public List<ProductQueryModel> Search(string value)
        {
            var inventory = _inventoryContext
               .Inventory
               .Select(x => new { x.ProductId, x.UnitPrice }).ToList();

            var discounts = _discountContext
                .CustomerDiscounts
                .Where(x => x.StartDate < DateTime.Now && x.EndDate > DateTime.Now)
                .Select(x => new { x.ProductId, x.DiscountRate, x.EndDate })
                .ToList();

            var query = _context
                .Products
                .Include(x => x.Category)
                .Select(x => new ProductQueryModel
                {
                    Id = x.Id,
                    Category = x.Category.Name,
                    Name = x.Name,
                    Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug,
                    CategorySlug = x.Category.Slug,
                    ShortDescription = x.ShortDescription,
                }).AsNoTracking();


            if (!string.IsNullOrWhiteSpace(value))
            {
                query = query
                    .Where(x => x.Name.Contains(value) || x.ShortDescription.Contains(value));
            }

            var products = query.OrderByDescending(x => x.Id).ToList();




            foreach (var product in products)
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
                        product.DiscountExpireDate = discount.EndDate.ToDiscountFormat();
                    }
                }
            }
            return products;
        }
    }
}
