using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.ProductAgg;

namespace ShopManagement.Infrastructure.EfCore.Mapping
{
    public class ProductMapping:IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Code).IsRequired().HasMaxLength(15);
            builder.Property(x => x.ShortDescription).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Picture).HasMaxLength(1000);
            builder.Property(x => x.PictureAlt).IsRequired().HasMaxLength(255);
            builder.Property(x => x.PictureTitle).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Keywords).IsRequired().HasMaxLength(100);
            builder.Property(x => x.MetaDescription).IsRequired().HasMaxLength(150);

            builder.Property(x => x.Slug).HasMaxLength(500).IsRequired();



            builder
                .HasOne(x => x.Category)
                .WithMany(x => x.Products)
                .HasForeignKey(x => x.CategoryId);



            builder
                .HasMany(x => x.ProductPictures)
                .WithOne(x => x.Product)
                .HasForeignKey(x => x.ProductId);

        }
    }
}
