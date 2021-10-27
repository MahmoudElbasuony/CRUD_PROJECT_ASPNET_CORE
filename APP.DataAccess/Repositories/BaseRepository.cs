using APP.Entities;
using APP.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APP.DataAccess.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected DbSet<T> Entities { get; private set; }
        protected AppDbContext _Context { get; private set; }
        public BaseRepository(AppDbContext context)
        {
            _Context = context ?? throw new ArgumentNullException(nameof(context));
            Entities = context.Set<T>();
        }
        public T Create(T entity) => Entities.Add(entity).Entity;
        public Task<T> GetById(int Id) => Entities.FindAsync(Id).AsTask();
        public T Update(T entity) => Entities.Update(entity).Entity;
        public Task<int> Save() => _Context.SaveChangesAsync();
        public virtual Task<List<T>> GetAll(int pageNumber = 0, int pageSize = 100)
                => Entities.AsQueryable().Skip(pageNumber * pageSize).Take(pageSize).ToListAsync();
        public async Task Delete(int Id)
        {
            var entity = await GetById(Id);
            if (entity != null)
                Entities.Remove(entity);
        }

    }
}
