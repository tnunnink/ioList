using ioList.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ioList.Data.Configurations
{
    public class PointCategoryConfiguration : IEntityTypeConfiguration<PointLabel>
    {
        public void Configure(EntityTypeBuilder<PointLabel> builder)
        {
            builder.HasKey(x => new { x.PointId, CategoryId = x.LabelId });
        }
    }
}