using InventoryManagement.Domain.InventoryAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InventoryManagement.Infrastructure.EfCore.Mapping
{
    public class InventoryMapping : IEntityTypeConfiguration<Inventory>
    {
        public void Configure(EntityTypeBuilder<Inventory> builder)
        {
            builder.ToTable("Inventory");
            builder.HasKey(x => x.Id);



            builder
                .OwnsMany(x => x.Operations, b =>
                {
                    b.HasKey(x => x.Id);
                    b.ToTable("InventoryOperations");
                    b.Property(x => x.Description).HasMaxLength(1000);


                    b.WithOwner(x => x.Inventory)
                        .HasForeignKey(x => x.InventoryId);
                });
        }
    }
}
