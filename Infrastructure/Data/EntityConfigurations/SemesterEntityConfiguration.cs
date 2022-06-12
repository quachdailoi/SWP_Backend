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
    public class SemesterEntityConfiguration : BaseEntityConfiguration<Semester>
    {
        public override void Configure(EntityTypeBuilder<Semester> builder)
        {
            base.Configure(builder);

            builder.ToTable("semesters");

            builder.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnName("name");

            builder.Property(e => e.Code)
                .IsRequired()
                .HasMaxLength(20)
                .HasColumnName("code");

            builder.Property(e => e.StartAt)
                .IsRequired()
                .HasColumnName("start_at");

            builder.Property(e => e.EndAt)
                .IsRequired()
                .HasColumnName("end_at");
        }
    }
}
