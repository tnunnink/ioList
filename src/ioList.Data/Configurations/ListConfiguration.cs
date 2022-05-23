using ioList.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ioList.Data.Configurations
{
    public class ListConfiguration : IEntityTypeConfiguration<List>
    {
        public void Configure(EntityTypeBuilder<List> builder)
        {
            builder.ToTable(nameof(List)).HasKey(x => x.Name);
            builder.Property(x => x.Directory).IsRequired();
            builder.Ignore(x => x.FullPath);
        }
    }
}