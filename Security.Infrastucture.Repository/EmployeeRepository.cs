using Security.Domain.Entities;
using Security.Domain.Interfaces.IRepository;
using Security.Infrastructure.DataModel.Context;

namespace Security.Infrastucture.Repository
{
    public class EmployeeRepository : GenericRepository<Employee, int>, IEmployeeRepository
    {
        public EmployeeRepository(MainContext mainContext) : base(mainContext)
        {

        }
    }
}
