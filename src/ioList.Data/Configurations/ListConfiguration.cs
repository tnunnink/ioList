using ioList.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ioList.Data.Configurations
{
    public class ListConfiguration : IEntityTypeConfiguration<List>
    {
        public void Configure(EntityTypeBuilder<List> builder)
        {
            builder.ToTable(nameof(List)).HasKey(x => x.Id);

            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Directory).IsRequired();
            
            builder.HasOne(x => x.Source).WithOne(x => x.List).HasForeignKey<ListSource>(x => x.ListId);
            builder.HasOne(x => x.Options).WithOne(x => x.List).HasForeignKey<ListOptions>(x => x.ListId);
        }
    }
}