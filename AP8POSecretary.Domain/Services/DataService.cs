using AP8POSecretary.Domain.Entities;
using AP8POSecretary.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AP8POSecretary.Domain.Services
{
    public class DataService<T> : IDataService<T>
    {
        private readonly IDataRepository<T> _dataRepository;
        public DataService(IDataRepository<T> dataRepository)
        {
            _dataRepository = dataRepository;
        }

        public Task AddRange(IEnumerable<GroupSubject> entities)
        {
            return _dataRepository.AddRange(entities);
        }

        public Task AddEmployeesRange(IEnumerable<Employee> entities)
        {
            return _dataRepository.AddEmployeesRange(entities);
        }

        public Task AddWorkingLabels(IEnumerable<WorkingLabel> entities)
        {
            return _dataRepository.AddWorkingLabels(entities);
        }

        public Task<T> Create(T entity)
        {
            return _dataRepository.Create(entity);
        }

        public Task<bool> Delete(int id)
        {
            return _dataRepository.Delete(id);
        }

        public Task<bool> DeleteAllGroupSubject()
        {
            return _dataRepository.DeleteAllGroupSubject();
        }

        public Task<T> Get(int id)
        {
            return _dataRepository.Get(id);
        }

        public Task<IEnumerable<T>> GetAll()
        {
            return _dataRepository.GetAll();
        }

        public Task<IEnumerable<Employee>> GetAllEmployees()
        {
            return _dataRepository.GetAllEmployees();
        }

        public Task<IEnumerable<Group>> GetAllGroups()
        {
            return _dataRepository.GetAllGroups();
        }

        public Task<IEnumerable<WorkingLabel>> GetAllWorkingLabels()
        {
            return _dataRepository.GetAllWorkingLabels();
        }

        public Task<T> Update(int id, T entity)
        {
            return _dataRepository.Update(id, entity);
        }

        public Task Update(IEnumerable<T> entities)
        {
            return _dataRepository.Update(entities);
        }

        public Task DeleteAll(IEnumerable<T> entities)
        {
            return _dataRepository.DeleteAll(entities);
        }
    }
}
