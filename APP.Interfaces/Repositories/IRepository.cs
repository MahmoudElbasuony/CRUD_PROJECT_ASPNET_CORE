using APP.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace APP.Interfaces.Repositories
{
    public interface IRepository<T>  where T : BaseEntity
    {
        T Create(T entity);
        Task Delete(int Id);
        T Update(T entity);
        Task<List<T>> GetAll(int pageNumber = 0, int pageSize = 100);
        Task<T> GetById(int Id);
        Task<int> Save();
    }
}
