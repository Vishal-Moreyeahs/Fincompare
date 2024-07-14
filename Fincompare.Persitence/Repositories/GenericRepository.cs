using Fincompare.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Fincompare.Persitence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly FincompareDbContext _context;
        private readonly DbSet<T> dbSet;

        public GenericRepository(
            FincompareDbContext context
        )
        {
            _context = context;
            this.dbSet = _context.Set<T>();
        }

        public virtual async Task<IEnumerable<T>> GetAll()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T> GetByStringId(string id)
        {
            try
            {
                return await dbSet.FindAsync(id);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public virtual async Task<T> GetById(int id)
        {
            try
            {
                return await dbSet.FindAsync(id);
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public virtual async Task<bool> Add(T entity)
        {
            try
            {
                await dbSet.AddAsync(entity);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public virtual async Task<bool> AddRange(IEnumerable<T> entities)
        {
            try
            {
                await dbSet.AddRangeAsync(entities);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }
        public virtual bool RemoveRange(IEnumerable<T> entities)
        {
            try
            {
                dbSet.RemoveRange(entities);
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public virtual async Task<bool> Delete(T entity)
        {
            try
            {
                if (entity != null)
                {
                    dbSet.Remove(entity);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }


        public virtual async Task<bool> Upsert(T entity)
        {
            try
            {
                if (entity != null)
                {
                    dbSet.Update(entity);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                return false;
            }
        }

        //Get All data with all foreign key related data.
        public IEnumerable<T> GetAllRelatedEntity()
        {
            var entityType = _context.Model.FindEntityType(typeof(T));

            var query = _context.Set<T>().AsQueryable();

            foreach (var navigation in entityType.GetNavigations())
            {
                query = query.Include(navigation.Name);
            }

            return query.ToList();
        }
    }
}
