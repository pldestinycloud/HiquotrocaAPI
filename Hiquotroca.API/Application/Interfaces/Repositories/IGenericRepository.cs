namespace Hiquotroca.API.Application.Interfaces.Repositories
{
    public interface IGenericRepository<T>
    {
        Task<T?> GetByIdAsync(long id);
        Task<IEnumerable<T>> GetAllAsync();
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
