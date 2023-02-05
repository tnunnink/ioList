using ioList.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ioList.Data.Configurations
{
    public class ImportSettingsConfiguration : IEntityTypeConfiguration<ImportSettings>
    {
        public void Configure(EntityTypeBuilder<ImportSettings> builder)
        {
            builder.ToTable(nameof(ImportSettings)).HasNoKey();
        }
    }
}