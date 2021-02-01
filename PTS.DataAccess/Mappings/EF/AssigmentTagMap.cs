using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PTS.Entities;

namespace PTS.DataAccess.Mappings.EF
{
    public class AssigmentTagMap : IEntityTypeConfiguration<AssigmentTag>
    {
        public void Configure(EntityTypeBuilder<AssigmentTag> builder)
        {
            builder.ToTable("AssigmentTags");
            builder.Property(x => x.TagBackgroundColor).IsRequired().HasMaxLength(7);
            builder.Property(x => x.TagColor).IsRequired().HasMaxLength(7);
        }
    }
}
