using Core.Base;

namespace Core.Interfaces;

public interface IGenericRepository<T> where T : Entity
{
    Task<T?> GetByIdAsync(int id);
    Task<IReadOnlyList<T>> ListAllAsync();
    Task<T?> GetEntityWithSpec(ISpecification<T> spec);
    Task<IReadOnlyList<T>> ListAsync(ISpecification<T> spec);
    
    Task<TResult?> GetEntityWithSpec<TResult>(ISpecification<T, TResult> spec);
    Task<IReadOnlyList<TResult>> ListAsync<TResult>(ISpecification<T, TResult> spec);
    
    void Add(T entity);
    Task<bool> AddAndSaveChangesAsync(T entity);
    void Update(T entity);
    void Remove(T entity);
    bool Exists(int id);
    Task<int> CountAsync(ISpecification<T> spec);
}
