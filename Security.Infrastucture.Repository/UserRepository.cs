using Security.Domain.Entities;
using Security.Domain.Interfaces.IRepository;
using Security.Infrastructure.DataModel.Context;

namespace Security.Infrastucture.Repository
{
    public class UserRepository : GenericRepository<User, int>, IUserRepository
    {
        public UserRepository(MainContext mainContext) : base(mainContext)
        {

        }
    }
}
