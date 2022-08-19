using Security.Domain.Entities;
using Security.Domain.Interfaces.IRepository;
using Security.Infrastructure.DataModel.Context;

namespace Security.Infrastucture.Repository
{
    public class MenuActionRepository : GenericRepository<MenuAction, int>, IMenuActionRepository
    {
        public MenuActionRepository(MainContext mainContext) : base(mainContext)
        {

        }
    }
}
