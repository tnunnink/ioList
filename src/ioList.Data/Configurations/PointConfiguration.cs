using ioList.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ioList.Data.Configurations
{
    public class PointConfiguration : IEntityTypeConfiguration<Point>
    {
        public void Configure(EntityTypeBuilder<Point> builder)
        {
            builder.ToTable(nameof(Point)).HasKey(x => x.Id);
            
            builder.Property(x => x.Address).IsRequired();

            builder.HasOne(x => x.Scaling).WithOne(x => x.Point).HasForeignKey<Scaling>(x => x.PointId);
            builder.HasMany(x => x.Labels).WithOne(x => x.Point).HasForeignKey(x => x.PointId);
            builder.HasMany(x => x.Comments).WithOne().HasForeignKey(x => x.PointId);
        }
    }
}