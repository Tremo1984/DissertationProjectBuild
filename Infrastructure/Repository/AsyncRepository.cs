#nullable disable
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using ApplicationCore.Exceptions;
using ApplicationCore.Interfaces;
using Infrastructure.DataAccess;
using Microsoft.EntityFrameworkCore;

[assembly:InternalsVisibleTo("XunitTests")]
namespace Infrastructure.Repository;

internal class AsyncRepository<T> : IAsyncRepository<T> where T : class
{    
    private readonly ApplicationDbContext _dbContext;    

    public AsyncRepository(ApplicationDbContext dbContext)
    {        
        _dbContext = dbContext;        
    }
        
    public async Task<IEnumerable<T>> FindAllAsync()
    {
        try
        {
            var entities = await _dbContext.Set<T>().ToListAsync();

            return await Task.FromResult(entities);
        }
        catch (Exception ex)
        {            
            throw new RepositoryException(ex.Message);
        }
    }
        
    public async Task<T> FindByIdAsync(int id)
    {
        try
        {
            var entity = await _dbContext.Set<T>().FindAsync(id);

            return await Task.FromResult(entity);
        }
        catch (Exception ex)
        {            
            throw new RepositoryException(ex.Message, ex);
        }
    }

    public async Task<T> FindByFuncAsync(Expression<Func<T, bool>> expression)
    {
        try
        {
            var entity = await _dbContext.Set<T>().FirstAsync(expression);

            return await Task.FromResult(entity);
        }
        catch (Exception ex)
        {            
            throw new RepositoryException(ex.Message);
        }
    }
        
    public async Task<T> CreateAsync(T entity)
    {
        if (entity == null)
        {
            return await Task.FromResult<T>(null);
        }

        try
        {
            var e = await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(e.Entity);
        }
        catch (Exception ex)
        {            
            throw new RepositoryException(ex.Message);
        }
    }
        
    public async Task<T> UpdateAsync(int id, T entity)
    {
        if (entity == null)
        {
            return await Task.FromResult<T>(null);
        }

        try
        {
            var e = _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(e.Entity);
        }
        catch (Exception ex)
        {            
            throw new RepositoryException(ex.Message, ex);
        }
    }
        
    public async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var entity = await FindByIdAsync(id);

            if (entity == null)
            {
                return await Task.FromResult<bool>(false);
            }

            _dbContext.Set<T>().Remove(entity);
            var removed = await _dbContext.SaveChangesAsync();

            return await Task.FromResult<bool>(removed > 0);
        }
        catch (Exception ex)
        {            
            throw new RepositoryException(ex.Message);
        }
    }

    public async Task<int> SaveAsync()
    {        
        return await _dbContext.SaveChangesAsync();
    }
}
