using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using WebApp.Core.Interfaces;
using WebApp.Infrastructure.Context;

namespace WebApp.Core.Services
{
    public class GenericRepository<T>: IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public virtual async Task<T> GetByIdAsync(long id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual async Task<List<T>> GetsAsync(
            params Expression<Func<T, object>>[] includes)
        {
            return await includes
                .Aggregate(
                   _dbContext.Set<T>().AsQueryable(),
                    (current, include) => current.Include(include)
                ).ToListAsync();
        }

        public virtual async Task<List<T>> GetsAsync(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes)
        {
            return await includes
               .Aggregate(
                   _dbContext.Set<T>().AsQueryable(),
                   (current, include) => current.Include(include),
                  c => c.Where(predicate)
               ).ToListAsync();
        }

        public virtual async Task<List<T>> GetsAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            params Expression<Func<T, object>>[] includes)
        {
            return await orderBy(includes
               .Aggregate(
                   _dbContext.Set<T>().AsQueryable(),
                   (current, include) => current.Include(include),
                  c => c.Where(predicate))
               ).ToListAsync();
        }

        public virtual async Task<List<TResult>> GetsAsync<TResult>(
          Expression<Func<T, bool>> predicate,
          Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
          Expression<Func<T, TResult>> selector,
          params Expression<Func<T, object>>[] includes)
        {
            return await orderBy(includes
               .Aggregate(
                   _dbContext.Set<T>().AsQueryable(),
                   (current, include) => current.Include(include),
                  c => c.Where(predicate))
               ).Select(selector).ToListAsync();
        }

        public virtual async Task<bool> AnyAsync(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes)
        {
            return await includes.Aggregate(
                _dbContext.Set<T>().AsQueryable(),
                (current, include) => current.Include(include),
                c => c.AnyAsync(predicate));
        }


        public virtual async Task<T> FirstOrDefaultAsync(
            Expression<Func<T, bool>> predicate,
            params Expression<Func<T, object>>[] includes)
        {
            return await includes
               .Aggregate(
               _dbContext.Set<T>().AsQueryable(),
               (current, include) => current.Include(include),
               c => c.FirstOrDefaultAsync(predicate));
        }

        public virtual async Task<T> FirstOrDefaultAsync(
            Expression<Func<T, bool>> predicate,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            return await orderBy(_dbContext.Set<T>()).FirstOrDefaultAsync(predicate).ConfigureAwait(false);
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<List<T>> AddAsync(List<T> entityList)
        {
            _dbContext.Set<T>().AddRange(entityList);
            await _dbContext.SaveChangesAsync();
            return entityList;
        }

        public virtual async Task<T> EditAsync(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<List<T>> EditAsync(List<T> entityList)
        {
            foreach (T item in entityList)
            {
                _dbContext.Entry(item).State = EntityState.Modified;
            }
            //_dbContext.Entry(entityList).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entityList;
        }


        public virtual async Task<T> EditAsync(long id, T entity)
        {
            var obj = await this.GetByIdAsync(id);

            if (obj == null)
                throw new NullReferenceException("Entity doesn't found.");

            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(List<T> entityList)
        {
            _dbContext.Set<T>().RemoveRange(entityList);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task DeleteAsync(long id)
        {
            var entity = await this.GetByIdAsync(id);
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

    }
}
