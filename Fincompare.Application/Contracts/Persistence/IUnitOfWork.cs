using Fincompare.Domain.Entities.Common;

namespace Fincompare.Application.Contracts.Persistence
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> GetRepository<T>() where T : ActionBase;
        Task<int> SaveChangesAsync();
    }
}
