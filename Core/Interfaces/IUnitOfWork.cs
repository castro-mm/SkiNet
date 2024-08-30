using Core.Base;

namespace Core.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IGenericRepository<TEntity> Repository<TEntity>() where TEntity : Entity;
    Task<bool> Complete();
}
