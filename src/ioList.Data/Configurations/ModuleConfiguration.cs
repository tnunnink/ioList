using ioList.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ioList.Data.Configurations
{
    public class ModuleConfiguration : IEntityTypeConfiguration<Module>
    {
        public void Configure(EntityTypeBuilder<Module> builder)
        {
            builder.ToTable(nameof(Module)).HasKey(x => x.Id);
            builder.Property(x => x.Name).IsRequired();
            builder.Property(x => x.Parent).IsRequired();
            builder.Property(x => x.CatalogNumber).IsRequired();
            builder.HasOne(m => m.Source).WithMany().HasForeignKey(t => t.SourceId);
            builder.HasOne(m => m.Info).WithMany().HasForeignKey(t => t.CatalogNumber);
        }
    }
}