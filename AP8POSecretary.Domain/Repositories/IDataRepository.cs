using AP8POSecretary.Domain.Entities;
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
        Task<IEnumerable<Group>> GetAllGroups();
        Task Update(IEnumerable<T> entities);
        Task AddRange(IEnumerable<GroupSubject> entities);
    }
}
