using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Accounts.Core.Repositories.Base;
using Accounts.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Accounts.Infrastructure.Repositories.Base
{
     public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly AccountsContext _dbContext;
        protected DbSet<T> _table;

        public Repository(AccountsContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _table = _dbContext.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _table.ToListAsync();
        }


        public async Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _table.Where(predicate).ToListAsync();
        }
        
        public async Task<T> GetFirstAsync(Expression<Func<T, bool>> predicate)
        {
            return await _table.Where(predicate).FirstOrDefaultAsync();
        }

        public virtual async Task<T> GetByIdAsync<K>(K id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
        {
            return await _table.Where(predicate).AnyAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                _dbContext.Set<T>().Add(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (DbUpdateException )
            {
                //SqlException innerException = ex.InnerException as SqlException;
                //if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
               // {
                //    throw new Exception("DuplicateEntityException");
                //}

                throw;
            }
        }

        public async Task UpdateAsync(T entity)
        {
            try
            {
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                //SqlException innerException = ex.InnerException as SqlException;
               // if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
               // {
                //    throw new Exception("DuplicateEntityException");
                //}

                throw;
            }
        }

        public async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }
    }
}