using ioList.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ioList.Data.Configurations
{
    public class ListSourceConfiguration : IEntityTypeConfiguration<ListSource>
    {
        public void Configure(EntityTypeBuilder<ListSource> builder)
        {
            builder.ToTable("Source").HasKey(x => x.ListId);
        }
    }
}