using AP8POSecretary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AP8POSecretary.Infrastructure.Data.Configurations
{
    public class GroupSubjectConfiguration : IEntityTypeConfiguration<GroupSubject>
    {
        public void Configure(EntityTypeBuilder<GroupSubject> builder)
        {
            builder.ToTable("GroupSubject", "Secretary");

            builder.HasKey(e => e.Id);

            builder.HasOne(e => e.Subject)
                .WithMany(e => e.GroupSubjects)
                .HasForeignKey(e => e.SubjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(e => e.Group)
                .WithMany(e => e.GroupSubjects)
                .HasForeignKey(e => e.GroupId)
                .OnDelete(DeleteBehavior.Cascade);


            /*builder.HasOne(e => e.Group)
                .WithMany(e => e.GroupSubjects)
                .HasForeignKey(e => e.GroupId);*/         
        }
    }
}
