using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PTS.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace PTS.DataAccess.Mappings.EF
{
    public class EmailTemplateMap : IEntityTypeConfiguration<EmailTemplate>
    {
        public void Configure(EntityTypeBuilder<EmailTemplate> builder)
        {
            builder.ToTable("EmailTemplates");
            builder.HasIndex(x => x.TemplateType).IsUnique();
        }
    }
}
