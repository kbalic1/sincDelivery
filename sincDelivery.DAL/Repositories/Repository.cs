using sincDelivery.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace sincDelivery.DAL.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly sincDeliveryEntities db;

        public Repository(sincDeliveryEntities context)
        {
            db = context;
        }

        public TEntity GetByID(int id)
        {
            return db.Set<TEntity>().Find(id);
        }

        public async Task<TEntity> GetByIDAsync(int id)
        {
            return await db.Set<TEntity>().FindAsync(id);
        }

        public TEntity Insert(TEntity entity)
        {
            return db.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            db.Set<TEntity>().Remove(entity);
        }

        public IEnumerable<TEntity> Find(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "")
        {
            IQueryable<TEntity> query = db.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public virtual void Update(TEntity entity)
        {
            db.Set<TEntity>().Attach(entity);
            db.Entry(entity).State = System.Data.Entity.EntityState.Modified;
        }

        public void Complete()
        {
            try
            {
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;
            }
        }

        public async Task<int> CompleteAsync()
        {
            try
            {
                return await db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                throw ex;
            }
        }
    }
}
