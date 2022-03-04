using ioList.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ioList.Configurations
{
    public class IoListConfiguration : IEntityTypeConfiguration<IoList>
    {
        public void Configure(EntityTypeBuilder<IoList> builder)
        {
            builder.HasKey(t => t.Id);
        }
    }
}