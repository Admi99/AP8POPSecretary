using AP8POSecretary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AP8POSecretary.Infrastructure.Data.Configurations
{
    public class WorkingLabelConfiguration : IEntityTypeConfiguration<WorkingLabel>
    {
        public void Configure(EntityTypeBuilder<WorkingLabel> builder)
        {
            builder.ToTable("WorkingLabel", "Secretary");

            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Employee)
                .WithMany(e => e.WorkingLabels)
                .HasForeignKey(e => e.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);  

            builder.HasOne(e => e.Subject)
                .WithMany()
                .IsRequired()
                .HasForeignKey(e => e.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
