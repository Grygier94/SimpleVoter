using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace SimpleVoter.Core.Repositories
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();
        void Add(TEntity entity);
        void Remove(TEntity entity);
        int Count();
        int Count(Expression<Func<TEntity, bool>> expression);
        IEnumerable<TEntity> FindBy(Expression<Func<TEntity, bool>> expression);
    }
}