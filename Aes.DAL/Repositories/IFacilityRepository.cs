using Aes.DAL.Models;
using System.Collections.Generic;

namespace Aes.DAL.Repositories
{
    public interface IFacilityRepository : IRepository<FacilityObject>
    {
        IEnumerable<FacilityObject> Search(string keyword);
        (IEnumerable<FacilityObject> Items, int Total) GetPaged(int page, int pageSize);
    }
}
