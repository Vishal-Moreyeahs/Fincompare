using Fincompare.Application.Contracts.Persistence;
using Fincompare.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace Fincompare.Persitence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : ActionBase
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
            return await dbSet.Where(x => !x.IsDeleted).ToListAsync();
        }

        public async Task<T> GetById(string id)
        {
            try
            {
                var entity = await dbSet.FindAsync(id);
                return (entity == null || entity.IsDeleted) ? null : entity;
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
                var entity = await dbSet.FindAsync(id);
                return (entity == null || entity.IsDeleted) ? null : entity;
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
                    dbSet.Entry(entity).State = EntityState.Modified;
                    //dbSet.Update(entity);
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

            var query = _context.Set<T>().Where(x => !x.IsDeleted).AsQueryable();

            foreach (var navigation in entityType.GetNavigations())
            {
                query = query.Include(navigation.Name);
            }

            return query.ToList();
        }

        public async Task<T> GetByPrimaryKeyWithRelatedEntitiesAsync<TKey>(TKey primaryKey)
        {
            var entityType = _context.Model.FindEntityType(typeof(T));

            var query = _context.Set<T>().Where(x => !x.IsDeleted).AsQueryable();

            foreach (var navigation in entityType.GetNavigations())
            {
                query = query.Include(navigation.Name);
            }

            // Assuming the primary key is a single key. For composite keys, adjust accordingly.
            var primaryKeyProperty = entityType.FindPrimaryKey().Properties.FirstOrDefault();

            if (primaryKeyProperty == null)
            {
                throw new InvalidOperationException("Primary key not found");
            }

            var keyValues = new object[] { primaryKey };
            return await query.SingleOrDefaultAsync(e => EF.Property<TKey>(e, primaryKeyProperty.Name).Equals(primaryKey));
        }

    }
}
