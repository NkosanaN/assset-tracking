using Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;

public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly DataContext _dataContext;

    public GenericRepository(DataContext dataContext)
    {
        _dataContext = dataContext ?? throw new ArgumentNullException(nameof(_dataContext));
    }
    public async Task<List<T>> GetAllAsync()
    {
        return await _dataContext.Set<T>().AsNoTracking().ToListAsync();
    }

    public async Task<T> GetByIdAsync(Guid id)
    {
        return await _dataContext.Set<T>().FindAsync(id);
    }

    public async Task<bool> CreateAsync(T entity)
    {
        await _dataContext.AddAsync(entity);

        var result = await _dataContext.SaveChangesAsync() > 0;

        return result;
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        _dataContext.Remove(entity);

        var result = await _dataContext.SaveChangesAsync() > 0;

        return result;
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        _dataContext.Entry(entity).State = EntityState.Modified;

        var result = await _dataContext.SaveChangesAsync() > 0;

        return result;
    }
}

