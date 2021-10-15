using System.Collections.Generic;
using System.Linq;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.contracts.Product;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class ProductRepository : RepositoryBase<long, Product>, IProductRepository
    {
        private readonly ShopContext _context;

        public ProductRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public Product GetProductWhitCategory(long id)
        {
            return _context.Products.Include(x => x.Category).FirstOrDefault(x => x.Id == id);
        }

        public EditProduct GetDetails(long id)
        {
          return _context.Products.Select(x => new EditProduct()
          {
              Id=x.Id,
              Name = x.Name,
              Code = x.Code,
              Slug = x.Slug,
              CategoryId = x.CategoryId,
              Description = x.Description,
              Keywords = x.Keywords,
              MetaDescription = x.MetaDescription,
              ShortDescription = x.ShortDescription, 
              PictureAlt = x.PictureAlt,
              PictureTitle = x.PictureTitle,

          }).FirstOrDefault(x=>x.Id==id);
        }

        public List<ProductViewModel> GetProducts()
        {
            return _context.Products.Select(x => new ProductViewModel()
                {
                    Id = x.Id,
                    Name = x.Name
                }
            ).ToList();
        }

        public List<ProductViewModel> Search(ProductSearchModel searchModel)
        {
            var query =
                _context.Products
                    .Include(x=>x.Category)
                    .Select(x => new ProductViewModel()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Code = x.Code,
                        CategoryId =x.CategoryId,
                        Picture = x.Picture,
                        Category = x.Category.Name,
                        CreationDate = x.CreationDate,
                    });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(x => x.Name == searchModel.Name);
            }
            if (!string.IsNullOrWhiteSpace(searchModel.Code))
            {
                query = query.Where(x => x.Code == searchModel.Code);
            }
            if (searchModel.CategoryId !=0)
            {
                query = query.Where(x => x.CategoryId == searchModel.CategoryId);
            }


            return query.OrderByDescending(x => x.Id).ToList();

        }
    }
}
    