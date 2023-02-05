using ioList.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ioList.Data.Configurations
{
    public class ScalingConfiguration : IEntityTypeConfiguration<Scaling>
    {
        public void Configure(EntityTypeBuilder<Scaling> builder)
        {
            builder.ToTable(nameof(Scaling)).HasKey(x => x.TagId);
        }
    }
}