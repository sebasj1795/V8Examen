using System.Threading.Tasks;

namespace Security.Domain.Interfaces.IUnitOfWork
{
    public interface IUnitOfWork
    {
        void Save();
        Task SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
    }
}
