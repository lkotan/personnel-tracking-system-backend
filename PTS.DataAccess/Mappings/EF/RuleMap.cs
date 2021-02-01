using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PTS.Entities;

namespace PTS.DataAccess.Mappings.EF
{
    public class RuleMap : IEntityTypeConfiguration<Rule>
    {
        public void Configure(EntityTypeBuilder<Rule> builder)
        {
            builder.ToTable("Rules");

            builder.HasOne(x => x.Role).WithMany(x => x.Rules).HasForeignKey(x => x.RoleId).OnDelete(DeleteBehavior.Cascade);

            builder.HasIndex(x => new { x.RoleId, x.ApplicationModule }).IsUnique();
        }
    }
}
