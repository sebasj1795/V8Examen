using Security.Domain.Entities;
using Security.Domain.Interfaces.IRepository;
using Security.Infrastructure.DataModel.Context;
using Security.Transversal.Common.Enum;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Security.Infrastucture.Repository
{
    public class MasterDetRepository : GenericRepository<MasterDet, int>, IMasterDetRepository
    {
        public MasterDetRepository(MainContext mainContext) : base(mainContext)
        {

        }

        public async Task<List<MasterDet>> GetByNameMaster(string name)
        {
            var credentials =
                await Find(p => p.Master.Equals(name) && p.State == (int)StateEnum.Active, true).ToListAsync();
            return credentials;
        }

        public async Task<string> GetValueByNameMaster(string masterName, string detailName)
        {
            var details = await Find(p => p.Master.Name == masterName && p.Name == detailName && p.State == (int)StateEnum.Active,true).FirstAsync();
            return details.Value;
        }

    }
}
