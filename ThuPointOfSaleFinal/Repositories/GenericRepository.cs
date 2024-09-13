using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using ThuPointOfSaleFinal.App.Interfaces;
using ThuPointOfSaleFinal.App.Models;
using ThuPointOfSaleFinal.Entities.Models;

namespace ThuPointOfSaleFinal.App.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private MyDbContext _db;
        private DbSet<T> _dbSet;
         public GenericRepository(MyDbContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }
        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _db.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetAsync(id);
            _db.Remove(entity);
            await _db.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null, 
            string[] includes = null)
        {
            IQueryable<T> query = _dbSet;
            if (includes != null) {
                foreach (var include in includes) { 
                    query = query.Include(include).AsSplitQuery();
                }
            }
            if (expression != null) {
                return await query.Where(expression).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<T> GetItemAsync(Expression<Func<T, bool>> expression = null, string[] includes = null)
        {
            IQueryable<T> query = _dbSet;
            if (includes != null)
            {
                foreach (var include in includes)
                {
                    query = query.Include(include).AsSplitQuery();
                }
            }
            if (expression != null)
            {
                return await query.Where(expression).FirstOrDefaultAsync();
            }

            return await query.FirstOrDefaultAsync();
        }

        public async Task<T> GetAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<T> UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
