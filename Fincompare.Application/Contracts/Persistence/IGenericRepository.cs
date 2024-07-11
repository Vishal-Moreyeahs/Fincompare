namespace Fincompare.Application.Contracts.Persistence
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);

        Task<bool> Add(T entity);

        Task<bool> AddRange(IEnumerable<T> entities);

        Task<bool> Delete(T id);

        Task<bool> Upsert(T entity);
        IEnumerable<T> GetAllRelatedEntity();
    }
}
