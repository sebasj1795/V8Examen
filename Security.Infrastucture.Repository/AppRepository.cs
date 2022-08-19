using Security.Domain.Entities;
using Security.Domain.Interfaces.IRepository;
using Security.Infrastructure.DataModel.Context;

namespace Security.Infrastucture.Repository
{
    public class AppRepository : GenericRepository<App, int>, IAppRepository
    {
        public AppRepository(MainContext mainContext) : base(mainContext)
        {

        }
    }
}
