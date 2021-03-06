﻿using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.XmlWrapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AP8POSecretary.Domain.Repositories
{
    public interface IDataRepository<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        Task<T> Create(T entity);
        Task<T> Update(int id, T entity);
        Task<bool> Delete(int id);
        Task DeleteAll(IEnumerable<T> entities);
        Task<IEnumerable<Group>> GetAllGroups();
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task<IEnumerable<Employee>> GetAllEmployeesWithSubjects();
        Task<IEnumerable<WorkingLabel>> GetAllWorkingLabels();
        Task Update(IEnumerable<T> entities);
        Task AddRange(IEnumerable<GroupSubject> entities);
        Task AddEmployeesRange(IEnumerable<Employee> entities);
        Task AddWorkingLabels(IEnumerable<WorkingLabel> entities);
        Task<bool> DeleteAllGroupSubject();
        Task AddAllIfTableEmpty(IEnumerable<T> entities);        
        bool Import(EntitiesWrapper entitiesWrapper);
    }
}
