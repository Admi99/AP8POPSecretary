using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.Repositories;
using AP8POSecretary.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP8POSecretary.Infrastructure.Repositories
{
    public class GenericRepository<T> : IDataRepository<T> where T : Entity
    {
        private readonly DataContextFactory _contextFactory;
        private readonly NonQueryDataService<T> _nonQueryDataService;

        public GenericRepository(DataContextFactory contextFactory)
        {
            _contextFactory = contextFactory;
            _nonQueryDataService = new NonQueryDataService<T>(contextFactory);
        }

        public async Task AddRange(IEnumerable<GroupSubject> entities)
        {
            using (DataContext context = _contextFactory.CreateDbContext())
            {
                await context.Set<GroupSubject>().AddRangeAsync(entities);
                await context.SaveChangesAsync();
            }
        }

        public async Task AddEmployeesRange(IEnumerable<Employee> entities)
        {
            using (DataContext context = _contextFactory.CreateDbContext())
            {
                await context.Set<Employee>().AddRangeAsync(entities);
                await context.SaveChangesAsync();
            }
        }

        public async Task AddWorkingLabels(IEnumerable<WorkingLabel> entities)
        {
            using (DataContext context = _contextFactory.CreateDbContext())
            {
                await context.Set<WorkingLabel>().AddRangeAsync(entities);
                await context.SaveChangesAsync();
            }
        }

        public async Task<T> Create(T entity)
        {
            return await _nonQueryDataService.Create(entity);
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

        public async Task<bool> DeleteAllGroupSubject()
        {
            using (DataContext context = _contextFactory.CreateDbContext())
            {
                var entities = await context.GroupSubjects.ToListAsync();
                context.GroupSubjects.RemoveRange(entities);
                await context.SaveChangesAsync();
                return true;
            }
        }

        public async Task<T> Get(int id)
        {
            using (DataContext context = _contextFactory.CreateDbContext())
            {
                T entity = await context.Set<T>().FirstOrDefaultAsync((e) => e.Id == id);
                await context.SaveChangesAsync();
                return entity;
            }
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            using (DataContext context = _contextFactory.CreateDbContext())
            {
                var entities = await context.Set<T>().ToListAsync();
                await context.SaveChangesAsync();
                return entities;
            }
        }

        public async Task<IEnumerable<Employee>> GetAllEmployees()
        {
            using (DataContext context = _contextFactory.CreateDbContext())
            {
                var entities = await context.Employees
                    .Include(item => item.WorkingLabels)
                    .ToListAsync();

                await context.SaveChangesAsync();
                return entities;
            }
        }

        public async Task<IEnumerable<Group>> GetAllGroups()
        {
            using (DataContext context = _contextFactory.CreateDbContext())
            {
                var entities = await context.Groups
                    .Include(item => item.GroupSubjects)
                    .ThenInclude(item => item.Subject)
                    .ToListAsync();

                await context.SaveChangesAsync();
                return entities;
            }
        }

        public async Task<IEnumerable<WorkingLabel>> GetAllWorkingLabels()
        {
            using (DataContext context = _contextFactory.CreateDbContext())
            {
                var entities = await context.WorkingLabels
                    .Include(item => item.Subject)
                    .Include(item => item.Employee)
                    .ToListAsync();

                await context.SaveChangesAsync();
                return entities;
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

        public async Task Update(IEnumerable<T> entities)
        {
            using (DataContext context = _contextFactory.CreateDbContext())
            {                                      
                context.Set<T>().UpdateRange(entities);
                await context.SaveChangesAsync();
            }
        }

        public async Task DeleteAll(IEnumerable<T> entities)
        {
            using (DataContext context = _contextFactory.CreateDbContext())
            {
                context.Set<T>().RemoveRange(entities);
                await context.SaveChangesAsync();
            }
        }

        public async Task AddAllIfTableEmpty(IEnumerable<T> entities)
        {
            using (DataContext context = _contextFactory.CreateDbContext())
            {
                if (!context.WorkingPointsWeights.Any())
                {
                    context.Set<T>().UpdateRange(entities);
                    await context.SaveChangesAsync();
                }               
            }
        }
    }
}


/*var haha = entities.Last() as Group;
               haha.GroupSubjects.RemoveAt(0);
               //haha.GroupSubjects.RemoveAt(0);
               context.Set<T>().Update(haha as T);*/