using ioList.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ioList.Data.Configurations
{
    public class SourceConfiguration : IEntityTypeConfiguration<Source>
    {
        public void Configure(EntityTypeBuilder<Source> builder)
        {
            builder.ToTable(nameof(Source)).HasKey(s => s.Id);
            builder.Property(s => s.FileName).IsRequired();
            builder.Property(s => s.Content).IsRequired();
            builder.Property(s => s.ImportedOn).IsRequired();
            builder.Property(s => s.ImportedBy).IsRequired();
        }
    }
}