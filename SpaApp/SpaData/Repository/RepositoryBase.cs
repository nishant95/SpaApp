using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace SpaData.Repository
{
    public class RepositoryBase<TDbContext, TEntity> : IRepository<TEntity>
        where TEntity : class
        where TDbContext : DbContext, new()
    {
        #region Privates and Constants
        private TDbContext _entities;
        #endregion

        #region Constructors
        public RepositoryBase(TDbContext context)
        {
            _entities = context;
        }
        #endregion

        #region Properties

        public TDbContext Context
        {
            get { return _entities; }
            set { _entities = value; }
        }

        #endregion

        #region Methods

        public TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public IQueryable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().AsQueryable();
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).AsQueryable();
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> includeProperties)
        {
            var context = Context.Set<TEntity>();

            var result = context.Include(includeProperties).Where(predicate);

            return result;
        }

        public IQueryable<TEntity> Find(Expression<Func<TEntity, bool>> predicate, string includeProperties)
        {
            //Context.Set<TEntity>().Include(includeProperties);
            //Context.Entry<TEntity>(TEntity).Reference(includeProperties).Load();

            var context = Context.Set<TEntity>();
            var result = context.Where(predicate);
            try
            {
                foreach (var item in result)
                {
                    Context.Entry(item).Collection(includeProperties).Load();
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return result;
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().SingleOrDefault(predicate);
        }

        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> predicate, Expression<Func<TEntity, object>> includeProperties)
        {
            return Context.Set<TEntity>().Include(includeProperties).SingleOrDefault(predicate);
        }

        public void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        #endregion
    }
}
