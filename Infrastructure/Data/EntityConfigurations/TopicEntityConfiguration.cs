using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.EntityConfigurations
{
    public class TopicEntityConfiguration : BaseEntityConfiguration<Topic>
    {
        public override void Configure(EntityTypeBuilder<Topic> builder)
        {
            base.Configure(builder);

            builder.ToTable("topics");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");

            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("code");

            builder.Property(e => e.Description)
                .IsRequired()
                .HasMaxLength(500)
                .HasColumnName("description");
        }
    }
}
