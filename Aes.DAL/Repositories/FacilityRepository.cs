using Microsoft.EntityFrameworkCore;
using Aes.DAL.Models;
using System.Collections.Generic;
using System.Linq;

namespace Aes.DAL.Repositories
{
    public class FacilityRepository : EFRepository<FacilityObject>, IFacilityRepository
    {
        public FacilityRepository(AesContext ctx) : base(ctx) { }

        public IEnumerable<FacilityObject> Search(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
                return _dbSet.AsNoTracking().ToList();

            return _dbSet.AsNoTracking()
                .Where(f => EF.Functions.Like(f.Name, $"%{keyword}%") || EF.Functions.Like(f.Type, $"%{keyword}%"))
                .ToList();
        }

        public (IEnumerable<FacilityObject> Items, int Total) GetPaged(int page, int pageSize)
        {
            var q = _dbSet.AsNoTracking().OrderBy(f => f.Name);
            var total = q.Count();
            var items = q.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return (items, total);
        }
    }
}
