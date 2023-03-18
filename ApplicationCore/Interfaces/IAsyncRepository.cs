namespace ApplicationCore.Interfaces;

public interface IAsyncRepository<T> where T : class
{
    Task<T> CreateAsync(T entity);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<T>> FindAllAsync();
    Task<T> FindByFuncAsync(System.Linq.Expressions.Expression<Func<T, bool>> expression);
    Task<T> FindByIdAsync(int id);
    Task<int> SaveAsync();
    Task<T> UpdateAsync(int id, T entity);
}
