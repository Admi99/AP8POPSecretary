﻿using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AP8POSecretary.Infrastructure.Repositories
{
    public class NonQueryDataService<T> where T : Entity
    {
        private readonly DataContextFactory _contextFactory;

        public NonQueryDataService(DataContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
        }

        public async Task<T> Create(T entity)
        {
            using (DataContext context = _contextFactory.CreateDbContext())
            {
                EntityEntry<T> createdResult = await context.Set<T>().AddAsync(entity);
                await context.SaveChangesAsync();

                return createdResult.Entity;
            }
         }

        public async Task<T> Update(int id, T entity)
        {
            using (DataContext context = _contextFactory.CreateDbContext())
            {
                entity.Id = id;

                context.Set<T>().Update(entity);
                await context.SaveChangesAsync();

                return entity;
            }
        }

        public async Task<bool> Delete(int id)
        {
            using (DataContext context = _contextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();

                return true;
            }
        }
     
    }
}
