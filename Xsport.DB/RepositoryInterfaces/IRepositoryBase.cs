using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Xsport.DB.RepositoryInterfaces
{
    public interface IRepositoryBase<T>
    {
        IQueryable<T> FindAll(bool trackChanges);
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        public IQueryable<T> FindAllWithEagerLoad(bool trackChanges, params Expression<Func<T, object>>[] navigationProperties);
        public IQueryable<T> FindByConditionWithEagerLoad(bool trackChanges, Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] navigationProperties);
    }
}
