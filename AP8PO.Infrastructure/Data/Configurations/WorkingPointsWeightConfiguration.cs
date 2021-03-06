﻿using AP8POSecretary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace AP8POSecretary.Infrastructure.Data.Configurations
{
    public class WorkingPointsWeightConfiguration : IEntityTypeConfiguration<WorkingPointsWeight>
    {
        public void Configure(EntityTypeBuilder<WorkingPointsWeight> builder)
        {
            builder.ToTable("WorkingPointsWeight", "Secretary");

            builder.HasKey(e => e.Id);
        }
    }
}
