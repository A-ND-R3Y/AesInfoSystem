using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace Aes.DAL.Repositories
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        protected readonly AesContext _context;
        protected readonly DbSet<T> _dbSet;

        public EFRepository(AesContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public IEnumerable<T> GetAll() => _dbSet.AsNoTracking().ToList();

        public T? Get(int id) => _dbSet.Find(id);

        public void Create(T entity) => _dbSet.Add(entity);

        public void Update(T entity) => _dbSet.Update(entity);

        public void Delete(int id)
        {
            var e = _dbSet.Find(id);
            if (e != null) _dbSet.Remove(e);
        }
    }
}
