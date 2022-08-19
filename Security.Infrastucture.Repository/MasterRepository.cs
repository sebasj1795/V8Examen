using Security.Domain.Entities;
using Security.Domain.Interfaces.IRepository;
using Security.Infrastructure.DataModel.Context;

namespace Security.Infrastucture.Repository
{
    public class MasterRepository : GenericRepository<Master, int>, IMasterRepository
    {
        public MasterRepository(MainContext mainContext) : base(mainContext)
        {
        }
    }
}
