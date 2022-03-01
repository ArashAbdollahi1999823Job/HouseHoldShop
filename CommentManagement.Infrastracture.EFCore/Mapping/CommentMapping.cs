using CommentManagement.Domain.CommentAgg;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommentManagement.Infrastructure.EFCore.Mapping
{
   public  class CommentMapping:IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
           builder.ToTable("Comments")
               .HasKey(x=>x.Id);


           builder.Property(x => x.Name).HasMaxLength(500);
           builder.Property(x => x.Email).HasMaxLength(500);
           builder.Property(x => x.Message).HasMaxLength(1000);
           builder.Property(x => x.Website).HasMaxLength(1000).IsRequired(required:false);








            builder
                .HasMany(x => x.Comments)
                .WithOne(x => x.Parent)
                .HasForeignKey(x => x.ParentId )
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
