using System;
using _01_Query.Contracts;
using _01_Query.Contracts.Product;
using _01_Query.Contracts.ProductCategory;
using _01_Query.Contracts.Slide;
using _01_Query.Query;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ShopManagement.Application;
using ShopManagement.Application.contracts.Product;
using ShopManagement.Application.contracts.ProductCategory;
using ShopManagement.Application.contracts.ProductPicture;
using ShopManagement.Application.contracts.Slider;
using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.SliderAgg;
using ShopManagement.Infrastructure.EfCore;
using ShopManagement.Infrastructure.EfCore.Repository;

namespace ShopManagement.Configuration
{
    public class ShopManagementBootstrapper
    {
        public static void Configure(IServiceCollection service,string connectionString)
        {
            service.AddTransient<IProductCategoryApplication, ProductCategoryApplication>();
            service.AddTransient<IProductCategoryRepository,ProductCategoryRepository>();


            service.AddTransient<IProductApplication, ProductApplication>();
            service.AddTransient<IProductRepository, ProductRepository>();


            service.AddTransient<IProductPictureApplication,ProductPictureApplication>();
            service.AddTransient<IProductPictureRepository,ProductPictureRepository>();


            service.AddTransient<ISliderApplication,SliderApplication>();
            service.AddTransient<ISliderRepository, SliderRepository>();

            
            service.AddTransient<ISlideQuery,SlideQuery>();


            service.AddTransient<IProductCategoryQuery, ProductCategoryQuery>();
            service.AddTransient<IProductQuery, ProductQuery>();



            service.AddDbContext<ShopContext>(x=>x.UseSqlServer(connectionString));
        }
    }
}
