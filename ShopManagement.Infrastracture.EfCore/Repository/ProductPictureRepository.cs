using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _0_Framework.Application;
using _0_Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using ShopManagement.Application.contracts.ProductPicture;
using ShopManagement.Domain.ProductPictureAgg;

namespace ShopManagement.Infrastructure.EfCore.Repository
{
    public class ProductPictureRepository : RepositoryBase<long, ProductPicture>, IProductPictureRepository
    {

        private readonly ShopContext _context;
        public ProductPictureRepository(ShopContext context) : base(context)
        {
            _context = context;
        }

        public EditProductPicture GetDetails(long id)
        {
            return _context.ProductPictures
                 .Select(x => new EditProductPicture()
                 {
                     Id = x.Id,
                    // Picture =  x.Picture,
                     PictureTitle = x.PictureTitle,
                     PictureAlt = x.PictureAlt,
                     ProductId = x.ProductId,
                 })
                 .FirstOrDefault(x => x.Id == id);
        }

        public ProductPicture GetWithProductAndCategory(long id)
        {
            return _context.ProductPictures.Include(x => x.Product)
                .ThenInclude(x=>x.Category)
                .FirstOrDefault(x => x.Id == id);
        }

        public List<ProductPictureViewModel> Search(ProductPictureSearchModel searchModel)
        {
            var query = _context.ProductPictures
                .Include(x=>x.Product)
                .Select(x => new ProductPictureViewModel()
                {
                    Id = x.Id,
                    Picture = x.Picture,
                    ProductId =x.ProductId, 
                    CreationDate = x.CreationDate.ToFarsi(),
                    Product = x.Product.Name,
                    IsRemoved = x.IsRemoved
                });


            if (searchModel.ProductId != 0)
            {
                query = query.Where(x => x.ProductId == searchModel.ProductId);
            }

            return query.OrderByDescending(x => x.Id).ToList();


        }
    }
}
