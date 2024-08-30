using System.Collections.Concurrent;
using Core.Base;
using Core.Interfaces;

namespace Infrastructure.Data;

public class UnitOfWork(StoreContext context) : IUnitOfWork
{
    private readonly ConcurrentDictionary<string, object> _repositories = new();

    public IGenericRepository<TEntity> Repository<TEntity>() where TEntity : Entity
    {
        var type = typeof(TEntity).Name;

        return (IGenericRepository<TEntity>)_repositories.GetOrAdd(type, t => 
        {
            var repoitoryType = typeof(GenericRepository<>).MakeGenericType(typeof(TEntity));
            return Activator.CreateInstance(repoitoryType, context) ?? throw new InvalidOperationException($"Could not create repository instance for {t}");
        });
    }

    public async Task<bool> Complete()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void Dispose()
    {
        context.Dispose();
    }

}
