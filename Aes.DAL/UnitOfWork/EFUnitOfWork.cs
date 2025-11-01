using Aes.DAL.Models;
using Aes.DAL.Repositories;

namespace Aes.DAL.UnitOfWork
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly AesContext _context;
        public IFacilityRepository Facilities { get; }
        public IRepository<Employee> Employees { get; }

        public EFUnitOfWork()
        {
            _context = new AesContext();
            Facilities = new FacilityRepository(_context);
            Employees = new EFRepository<Employee>(_context);
        }

        public void Save() => _context.SaveChanges();
        public void Dispose() => _context.Dispose();
    }
}
