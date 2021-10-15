using System.Collections.Generic;
using System.Linq;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using ShopManagement.Application.contracts.ProductCategory;
using ShopManagement.Domain.ProductCategoryAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class ProductCategoryRepository:RepositoryBase<long,ProductCategory>,IProductCategoryRepository
    {

        private readonly ShopContext _context;

        public ProductCategoryRepository(ShopContext context):base(context)
        {
            _context = context;
        }

        public List<ProductCategoryViewModel> GetProductCategories()
        {
            return _context.ProductCategories
                .Select(x => new ProductCategoryViewModel()
                {
                    Id=x.Id,
                    Name=x.Name
                })
                .ToList();
        }

        public EditProductCategory GetDetailsBy(long id)
        {
            return _context.ProductCategories
                .Select(x => new EditProductCategory()
                {
                    Id = x.Id,
                    Description = x.Description,
                    Keywords = x.Keywords,
                    MetaDescription = x.MetaDescription,
                    Name = x.Name,
                    //Picture = x.Picture,
                    PictureAlt = x.PictureAlt,
                    PictureTitle = x.PictureTitle,
                    Slug = x.Slug
                })
                .FirstOrDefault(x => x.Id == id);
        }
        public List<ProductCategoryViewModel> Search(ProductCategorySearchModel searchModel)
        {
            var query=
                _context.ProductCategories.Select(x => new ProductCategoryViewModel()
            {
                    Id=x.Id,
                    Picture = x.Picture,
                    Name = x.Name,
                    CreationDate = x.CreationDate.ToFarsi()
            });

            if (!string.IsNullOrWhiteSpace(searchModel.Name))
            {
                query = query.Where(x => x.Name.Contains(searchModel.Name));
            }

            return query.OrderByDescending(x => x.Id).ToList();

        }
    }
}
