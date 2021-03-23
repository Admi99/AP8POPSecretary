﻿using AP8POSecretary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AP8POSecretary.Infrastructure.Data.Configurations
{
    public class SubjectConfiguration : IEntityTypeConfiguration<Subject>
    {
        public void Configure(EntityTypeBuilder<Subject> builder)
        {
            builder.ToTable("Employee", "Secretary");

            builder.HasKey(e => e.Id);

            /*builder.HasMany(e => e.Groups)
                .WithMany(e => e.Subjects);*/

        }
    }
}
