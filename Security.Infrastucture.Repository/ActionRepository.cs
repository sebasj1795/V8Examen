using Security.Domain.Entities;
using Security.Domain.Interfaces.IRepository;
using Security.Infrastructure.DataModel.Context;

namespace Security.Infrastucture.Repository
{
    public class ActionRepository : GenericRepository<Action, int>, IActionRepository
    {
        public ActionRepository(MainContext mainContext) : base(mainContext)
        {

        }
    }
}
