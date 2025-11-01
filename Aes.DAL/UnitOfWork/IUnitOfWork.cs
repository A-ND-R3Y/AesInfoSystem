using Aes.DAL.Models;
using Aes.DAL.Repositories;

namespace Aes.DAL.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IFacilityRepository Facilities { get; }
        IRepository<Employee> Employees { get; }
        void Save();
    }
}
