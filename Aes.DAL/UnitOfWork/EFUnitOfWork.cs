using Aes.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using Aes.DAL.Repositories;


namespace Aes.DAL.UnitOfWork
{
    public class EFUnitOfWork : IDisposable
    {
        private readonly AesContext _context;
        private EFRepository<FacilityObject>? _facilityRepo;
        private EFRepository<Employee>? _employeeRepo;

        public EFUnitOfWork()
        {
            _context = new AesContext();
        }

        public EFRepository<FacilityObject> FacilityObjects
        {
            get
            {
                if (_facilityRepo == null)
                    _facilityRepo = new EFRepository<FacilityObject>(_context);
                return _facilityRepo;
            }
        }

        public EFRepository<Employee> Employees
        {
            get
            {
                if (_employeeRepo == null)
                    _employeeRepo = new EFRepository<Employee>(_context);
                return _employeeRepo;
            }
        }

        public void Save() => _context.SaveChanges();

        public void Dispose() => _context.Dispose();
    }
}
