using Security.Domain.Entities;
using Security.Domain.Interfaces.IRepository;
using Security.Infrastructure.DataModel.Context;

namespace Security.Infrastucture.Repository
{
    public class MenuRepository : GenericRepository<Menu, int>, IMenuRepository
    {
        public MenuRepository(MainContext mainContext) : base(mainContext)
        {

        }
    }
}
