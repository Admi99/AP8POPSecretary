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

        public Task<T> Create(T entity)
        {
            return _dataRepository.Create(entity);
        }

        public Task<bool> Delete(int id)
        {
            return _dataRepository.Delete(id);
        }

        public Task<T> Get(int id)
        {
            return _dataRepository.Get(id);
        }

        public Task<IEnumerable<T>> GetAll()
        {
            return _dataRepository.GetAll();
        }

        public Task<T> Update(int id, T entity)
        {
            return _dataRepository.Update(id, entity);
        }
    }
}
