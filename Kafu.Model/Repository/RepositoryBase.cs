using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using X.PagedList;
namespace Kafu.Model.Repository
{
    public class RepositoryBase<TEntity> where TEntity : class
    {
        internal Kafu_SystemContext context;
        internal DbSet<TEntity> dbSet;

        public RepositoryBase(Kafu_SystemContext context)
        {
            this.context = context;
            this.dbSet = context.Set<TEntity>();
        }
    
        public virtual List<TEntity> Find(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            var x = dbSet.FirstOrDefault(); 

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
                return orderBy(query).ToList();
            else
                return query.ToList();
        }

        public virtual IQueryable<TEntity> FindIQueryable(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
                return orderBy(query);
            else
                return query;
        }

        public virtual async Task<List<TEntity>> FindAsync(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "")
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
                return await orderBy(query).ToListAsync();
            else
                return await query.ToListAsync();
        }

        public virtual async Task<IPagedList<TEntity>> FindByPagedAsync(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "", int pageNum = 1, int pageSize = 10)
        {
            IQueryable<TEntity> query = dbSet;

            if (filter != null)
                query = query.Where(filter);

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
                return await orderBy(query).ToPagedListAsync(pageNum, pageSize);
            else
                return await query.ToPagedListAsync(pageNum, pageSize);
        }

        public virtual TEntity FindById(object id)
        {
            return dbSet.Find(id);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter != null)
                return await dbSet.CountAsync(filter);
            return await dbSet.CountAsync();
        }
        public virtual int Count(Expression<Func<TEntity, bool>> filter = null)
        {
            if (filter != null)
                return dbSet.Count(filter);
            return dbSet.Count();
        }
        public virtual async Task<TEntity> FindByIdAsync(object id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual TEntity First(Expression<Func<TEntity, bool>> predicate)
        {
            return context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public virtual async Task<TEntity> FirstAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await context.Set<TEntity>().FirstOrDefaultAsync(predicate);
        }
        public virtual void Insert(TEntity entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = dbSet.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
                dbSet.Attach(entityToDelete);
            dbSet.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public virtual async Task<int> SaveChangeAsync()
        {
            return await context.SaveChangesAsync();
        }

        public virtual int SaveChange()
        {
            return context.SaveChanges();
        }
    }
}
