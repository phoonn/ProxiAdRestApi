using System;
using System.Collections.Generic;
using System.Linq;
using WebStore.Models;
using System.Data.Entity;
using System.Linq.Expressions;

namespace WebStore.Services
{
    public class BaseRepository<TEntity> where TEntity : class
    {
        internal WebStoreContext context;
        internal DbSet<TEntity> Items;

        public BaseRepository(WebStoreContext context)
        {
            this.context = context;
            this.Items = context.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "",int take=0)
        {
            IQueryable<TEntity> query = Items;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }
            if (take>0)
            {
                if (orderBy != null)
                {
                    return orderBy(query).Take(take).ToList();
                }
                else
                {
                    return query.Take(take).ToList();
                }
            }
            else if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
            
        }

        public virtual TEntity GetByID(object id)
        {
            return Items.Find(id);
        }

        public virtual void Insert(TEntity entity)
        {
            Items.Add(entity);
        }

        public virtual void Delete(object id)
        {
            TEntity entityToDelete = Items.Find(id);
            Delete(entityToDelete);
        }

        public virtual void Delete(TEntity entityToDelete)
        {
            if (context.Entry(entityToDelete).State == EntityState.Detached)
            {
                Items.Attach(entityToDelete);
            }
            Items.Remove(entityToDelete);
        }

        public virtual void Update(TEntity entityToUpdate)
        {
            Items.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }
    }
}