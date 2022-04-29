using ioList.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ioList.Data.Configurations
{
    public class ListOptionsConfiguration : IEntityTypeConfiguration<ListOptions>
    {
        public void Configure(EntityTypeBuilder<ListOptions> builder)
        {
            builder.ToTable("Options").HasKey(x => x.ListId);
        }
    }
}