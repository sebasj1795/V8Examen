using Security.Domain.Entities;
using Security.Domain.Interfaces.IRepository;
using Security.Infrastructure.DataModel.Context;

namespace Security.Infrastucture.Repository
{
    public class ModuleRepository : GenericRepository<Module, int>, IModuleRepository
    {
        public ModuleRepository(MainContext mainContext) : base(mainContext)
        {

        }
    }
}
