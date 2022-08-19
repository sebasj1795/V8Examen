using Security.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Security.Domain.Interfaces.IRepository
{
    public interface IMasterDetRepository : IRepository<MasterDet, int>
    {
        public Task<List<MasterDet>> GetByNameMaster(string name);
        public Task<string> GetValueByNameMaster(string masterName, string detailName);
    }
}
