using System;
using ioList.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ioList.Data.Configurations
{
    public class ModuleInfoConfiguration : IEntityTypeConfiguration<ModuleInfo>
    {
        public void Configure(EntityTypeBuilder<ModuleInfo> builder)
        {
            builder.ToTable(nameof(ModuleInfo)).HasKey(m => m.CatalogNumber);

            /*builder.HasMany(i => i.Categories).WithOne().HasForeignKey(c => c.CatalogNumber);*/
            
            builder.Property(i => i.Categories).HasConversion(
                c => string.Join(',', c),
                c => c.Split(',', StringSplitOptions.RemoveEmptyEntries));
            
            builder.Property(i => i.Revisions).HasConversion(
                    r => string.Join(',', r),
                    r => r.Split(',', StringSplitOptions.RemoveEmptyEntries));
            
            builder.Property(i => i.PortTypes).HasConversion(
                p => string.Join(',', p),
                p => p.Split(',', StringSplitOptions.RemoveEmptyEntries));
        }
    }
}