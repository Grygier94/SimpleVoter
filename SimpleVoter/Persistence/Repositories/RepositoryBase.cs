using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using SimpleVoter.Core.Models;
using SimpleVoter.Core.Repositories;

namespace SimpleVoter.Persistence.Repositories
{
    public abstract class RepositoryBase<TEntity> : IRepositoryBase<TEntity> where TEntity : class
    {
        protected readonly ApplicationDbContext Context;

        protected RepositoryBase(ApplicationDbContext context)
        {
            Context = context;
        }

        public virtual TEntity Get(int id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public virtual void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public virtual void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public virtual int Count()
        {
            return Context.Set<TEntity>().Count();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> expression)
        {
            return Context.Set<TEntity>().Where(expression).Count();
        }

        public virtual IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> expression)
        {
            return Context.Set<TEntity>().Where(expression);
        }
    }
}