using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aes.DAL.Repositories
{
    public class EFRepository<T> where T : class
    {
        protected readonly DbContext _context;
        protected readonly DbSet<T> _dbSet;

        public EFRepository(DbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        // --- Базові CRUD ---
        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public T? Get(int id)
        {
            return _dbSet.Find(id);
        }

        public void Create(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            var entity = _dbSet.Find(id);
            if (entity != null)
                _dbSet.Remove(entity);
        }

        // --- 🔍 Додаткові методи пошуку ---
        // Пошук об'єктів за типом (для FacilityObject)
        public IEnumerable<T> FindByType(string type)
        {
            var prop = typeof(T).GetProperty("Type");
            if (prop == null) return Enumerable.Empty<T>();

            return _dbSet
                .AsEnumerable()
                .Where(e => (prop.GetValue(e)?.ToString() ?? "")
                .Contains(type, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        // Пошук об'єктів за місцем (Location)
        public IEnumerable<T> FindByLocation(string location)
        {
            var prop = typeof(T).GetProperty("Location");
            if (prop == null) return Enumerable.Empty<T>();

            return _dbSet
                .AsEnumerable()
                .Where(e => (prop.GetValue(e)?.ToString() ?? "")
                .Contains(location, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
