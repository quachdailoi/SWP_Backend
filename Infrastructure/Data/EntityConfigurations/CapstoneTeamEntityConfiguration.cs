using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.EntityConfigurations
{
    public class CapstoneTeamEntityConfiguration : BaseEntityConfiguration<CapstoneTeam>
    {
        public override void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<CapstoneTeam> builder)
        {
            base.Configure(builder);

            builder.ToTable("capstone_teams");

            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("code");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasColumnName("status");

            builder.Property(e => e.SemesterId)
                .IsRequired()
                .HasColumnName("semester_id");

            builder.Property(e => e.TopicId)
                .IsRequired()
                .HasColumnName("topic_id");

            // foreign keys
            builder.HasOne(e => e.Semester)
                .WithMany(r => r.CapstoneTeams)
                .HasForeignKey(r => r.SemesterId);

            builder.HasOne(e => e.Topic)
                .WithOne(r => r.CapstoneTeam)
                .HasForeignKey<CapstoneTeam>(c => c.TopicId)
                .IsRequired(false);
        }
    }
}
