using ioList.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ioList.Data.Configurations
{
    public class TagConfiguration : IEntityTypeConfiguration<Tag>
    {
        public void Configure(EntityTypeBuilder<Tag> builder)
        {
            builder.ToTable(nameof(Tag)).HasKey(x => x.Id);
            builder.Property(t => t.Address).IsRequired();
            builder.Property(t => t.DataType).IsRequired();
            builder.HasOne(t => t.Module).WithMany().HasForeignKey(t => t.ModuleId);
            builder.HasOne(t => t.Scaling).WithOne(x => x.Tag).HasForeignKey<Scaling>(x => x.TagId);
            builder.HasMany(t => t.References).WithOne(r => r.Tag).HasForeignKey(t => t.TagId);
            builder.HasMany(t => t.Labels).WithOne(x => x.Tag).HasForeignKey(x => x.TagId);
            builder.HasMany(t => t.Comments).WithOne().HasForeignKey(x => x.TagId);
        }
    }
}