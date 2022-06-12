using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Data.EntityConfigurations
{
    public class RoleUserEntityConfiguration : BaseEntityConfiguration<RoleUser>
    {
        public override void Configure(EntityTypeBuilder<RoleUser> builder)
        {
            base.Configure(builder);

            builder.ToTable("role_users");

            builder.Property(e => e.RoleId)
                .IsRequired()
                .HasColumnName("role_id");

            builder.Property(e => e.UserId)
                .IsRequired()
                .HasColumnName("user_id");

            builder.Property(e => e.ExaminationCouncilId)
                .IsRequired(false)
                .HasColumnName("examination_council_id");

            builder.Property(e => e.CapstoneTeamId)
                .IsRequired(false)
                .HasColumnName("capstone_team_id");

            // foreign keys
            builder.HasOne(e => e.Role)
                .WithMany(r => r.RoleUsers)
                .HasForeignKey(e => e.RoleId);

            builder.HasOne(e => e.User)
                .WithMany(r => r.RoleUsers)
                .HasForeignKey(e => e.UserId);
        }
    }
}
