﻿using AP8POSecretary.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace AP8POSecretary.Infrastructure.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Group> Groups { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<WorkingLabel> WorkingLabels { get; set; }
        public DbSet<GroupSubject> GroupSubjects { get; set; }

        public DataContext(DbContextOptions options) : base(options) { }

    }
}
