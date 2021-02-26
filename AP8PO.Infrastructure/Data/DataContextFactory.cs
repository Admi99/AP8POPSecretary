﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;

namespace AP8POSecretary.Infrastructure.Data
{
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args = null)
        {
            var options = new DbContextOptionsBuilder<DataContext>();
            options.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=SecretaryTest;Trusted_Connection=True;");
            return new DataContext(options.Options);
        }
    }
}