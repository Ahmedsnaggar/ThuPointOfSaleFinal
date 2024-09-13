using System.Linq.Expressions;

namespace ThuPointOfSaleFinal.App.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> expression = null,
            string[] includes = null);
        Task<T> GetItemAsync(Expression<Func<T, bool>> expression = null,
            string[] includes = null);
        Task<T> GetAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task DeleteAsync(int id);
    }
}
