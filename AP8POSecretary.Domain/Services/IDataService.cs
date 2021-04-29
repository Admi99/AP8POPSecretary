using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.XmlWrapper;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AP8POSecretary.Domain.Services
{
    public interface IDataService<T>
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> Get(int id);
        Task<T> Create(T entity);
        Task<T> Update(int id, T entity);
        Task Update(IEnumerable<T> entities);
        Task<bool> Delete(int id);
        Task DeleteAll(IEnumerable<T> entities);
        Task<IEnumerable<Group>> GetAllGroups();
        Task AddRange(IEnumerable<GroupSubject> entities);
        Task<bool> DeleteAllGroupSubject();
        Task<IEnumerable<Employee>> GetAllEmployees();
        Task AddWorkingLabels(IEnumerable<WorkingLabel> entities);
        Task<IEnumerable<WorkingLabel>> GetAllWorkingLabels();
        Task AddEmployeesRange(IEnumerable<Employee> entities);
        Task AddAllIfTableEmpty(IEnumerable<T> entities);
        bool Import(EntitiesWrapper entitiesWrapper);
        Task<IEnumerable<Employee>> GetAllEmployeesWithSubjects();
    }
}
