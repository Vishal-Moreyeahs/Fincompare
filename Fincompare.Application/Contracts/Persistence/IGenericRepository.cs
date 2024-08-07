using Fincompare.Domain.Entities.Common;

namespace Fincompare.Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : ActionBase
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);

        Task<bool> Add(T entity);
        Task<T> GetById(string id);
        Task<bool> AddRange(IEnumerable<T> entities);
        bool UpdateRange(IEnumerable<T> entities);
        bool RemoveRange(IEnumerable<T> entities);
        Task<bool> Delete(T id);

        Task<bool> Upsert(T entity);
        IEnumerable<T> GetAllRelatedEntity();
        Task<T> GetByPrimaryKeyWithRelatedEntitiesAsync<TKey>(TKey primaryKey);
    }
}
