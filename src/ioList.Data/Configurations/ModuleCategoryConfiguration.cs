/*using ioList.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ioList.Data.Configurations
{
    public class ModuleCategoryConfiguration : IEntityTypeConfiguration<ModuleCategory>
    {
        public void Configure(EntityTypeBuilder<ModuleCategory> builder)
        {
            builder.ToTable(nameof(ModuleCategory)).HasKey(c => c.Id);
            builder.Property(c => c.CatalogNumber).IsRequired();
            builder.Property(c => c.Name).IsRequired();
        }
    }
}*/