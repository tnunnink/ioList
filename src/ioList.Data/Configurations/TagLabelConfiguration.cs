using ioList.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ioList.Data.Configurations
{
    public class TagLabelConfiguration : IEntityTypeConfiguration<TagLabel>
    {
        public void Configure(EntityTypeBuilder<TagLabel> builder)
        {
            builder.HasKey(x => new { TagId = x.TagId, LabelId = x.LabelId });
        }
    }
}