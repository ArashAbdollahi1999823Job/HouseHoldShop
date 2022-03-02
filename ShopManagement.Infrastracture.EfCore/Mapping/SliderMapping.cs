using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShopManagement.Domain.SliderAgg;

namespace ShopManagement.Infrastructure.EfCore.Mapping
{
    public class SliderMapping:IEntityTypeConfiguration<Slider>
    {
        public void Configure(EntityTypeBuilder<Slider> builder)
        {
            builder.ToTable("Sliders");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Picture).IsRequired().HasMaxLength(1000);
            builder.Property(x => x.PictureAlt).IsRequired().HasMaxLength(500);
            builder.Property(x => x.PictureTitle).IsRequired().HasMaxLength(500);
            builder.Property(x => x.Heading).IsRequired().HasMaxLength(255);
            builder.Property(x => x.Title).HasMaxLength(255);
            builder.Property(x => x.Text).HasMaxLength(255);
            builder.Property(x => x.BtnText).IsRequired().HasMaxLength(50);


        }
    }
}
