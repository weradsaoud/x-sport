using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Xsport.DB;
using Xsport.DB.RepositoryInterfaces;

namespace Xsport.DB.Repositories
{
    public class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
        protected AppDbContext _db { get; set; }
        public RepositoryBase(AppDbContext db)
        {
            _db = db;
        }
        public IQueryable<T> FindAll(bool trackChanges) => trackChanges ?
            _db.Set<T>() :
            _db.Set<T>().AsNoTracking();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression, bool trackChanges) =>
            trackChanges ?
            _db.Set<T>().Where(expression) :
            _db.Set<T>().Where(expression).AsNoTracking();
        public IQueryable<T> FindAllWithEagerLoad(bool trackChanges, params Expression<Func<T, object>>[] navigationProperties)
        {
            if (navigationProperties == null)
                throw new ArgumentNullException(nameof(navigationProperties));
            IQueryable<T> query = _db.Set<T>();
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                query = query.Include<T, object>(navigationProperty);
            return trackChanges ? query : query.AsNoTracking();
        }

        public IQueryable<T> FindByConditionWithEagerLoad(bool trackChanges, Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] navigationProperties)
        {
            if (navigationProperties == null || expression == null)
                throw new ArgumentNullException(nameof(navigationProperties));
            IQueryable<T> query = _db.Set<T>().Where(expression);
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                query = query.Include<T, object>(navigationProperty);
            return trackChanges ? query : query.AsNoTracking();
        }
        public void Create(T entity) => _db.Set<T>().Add(entity);
        public void Update(T entity) => _db.Set<T>().Update(entity);
        public void Delete(T entity) => _db.Set<T>().Remove(entity);

        public Task<IQueryable<T>> FindAllAsync(bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public Task<IQueryable<T>> FindByConditionAsync(Expression<Func<T, bool>> expression, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public async Task CreateAsync(T entity)
        {
            await _db.Set<T>().AddAsync(entity);
        }
        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
