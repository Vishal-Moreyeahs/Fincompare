using Fincompare.Application.Contracts.Persistence;
using Fincompare.Domain.Entities.Common;

namespace Fincompare.Persitence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FincompareDbContext _dbContext;

        public UnitOfWork(FincompareDbContext context)
        {
            _dbContext = context;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public IGenericRepository<T> GetRepository<T>() where T : ActionBase
        {
            return new GenericRepository<T>(_dbContext);
        }




    }
}
