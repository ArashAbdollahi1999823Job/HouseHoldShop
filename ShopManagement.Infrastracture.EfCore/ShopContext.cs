﻿using Microsoft.EntityFrameworkCore;

using ShopManagement.Domain.ProductAgg;
using ShopManagement.Domain.ProductCategoryAgg;
using ShopManagement.Domain.ProductPictureAgg;
using ShopManagement.Domain.SliderAgg;
using ShopManagement.Infrastructure.EfCore.Mapping;

namespace ShopManagement.Infrastructure.EfCore
{
   public class ShopContext:DbContext
    {
        public DbSet<ProductPicture> ProductPictures { set; get; }
        public DbSet<Product> Products { set; get; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public ShopContext(DbContextOptions<ShopContext> options):base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
             

            var assembly = typeof(ProductCategoryMapping).Assembly;

            modelBuilder.ApplyConfigurationsFromAssembly(assembly);



            base.OnModelCreating(modelBuilder);
        }
    }
}
