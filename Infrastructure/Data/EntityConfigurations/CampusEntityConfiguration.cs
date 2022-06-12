using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Domain.Entities;

namespace Infrastructure.Data.EntityConfigurations
{
    public class CampusEntityConfiguration : BaseEntityConfiguration<Campus>
    {
        public override void Configure(EntityTypeBuilder<Campus> builder)
        {
            base.Configure(builder);

            builder.ToTable("campuses");

            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("code");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");
        }
    }
}
