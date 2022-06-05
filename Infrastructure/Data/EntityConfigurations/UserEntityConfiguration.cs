using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SECapstoneEvaluation.Domain.Entities;

namespace SECapstoneEvaluation.Infrastructure.Data.EntityConfigurations
{
    public class UserEntityConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.ToTable("users");

            builder.Property(e => e.CampusId)
                .IsRequired()
                .HasColumnName("campus_id");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");

            builder.Property(e => e.Email)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("email");

            builder.Property(e => e.Gender)
                .IsRequired()
                .HasColumnName("gender");

            builder.Property(e => e.Birthday)
                .IsRequired()
                .HasColumnName("birthday");

            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("code");

            builder.Property(e => e.Phone)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("phone");

            builder.Property(e => e.Status)
                .IsRequired()
                .HasColumnName("status");

            // foreign key
            builder.HasOne(e => e.Campus)
                .WithMany(e => e.Users)
                .HasForeignKey(e => e.CampusId)
                .IsRequired();
        }
    }
}
