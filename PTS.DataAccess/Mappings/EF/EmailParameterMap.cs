using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PTS.Entities;

namespace PTS.DataAccess.Mappings.EF
{
    public class EmailParameterMap : IEntityTypeConfiguration<EmailParameter>
    {
        public void Configure(EntityTypeBuilder<EmailParameter> builder)
        {
            builder.ToTable("EmailParameters");
            builder.Property(x => x.SmtpServer).HasMaxLength(255);
            builder.Property(x => x.UserName).HasMaxLength(100);
            builder.Property(x => x.Password).HasMaxLength(50);
        }
    }
}
