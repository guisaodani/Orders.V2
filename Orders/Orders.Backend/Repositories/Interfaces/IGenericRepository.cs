using Orders.shared.Responses;

namespace Orders.Backend.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<ActionResponses<T>> GetAsync(int id);

    Task<ActionResponses<IEnumerable<T>>> GetAsync();

    Task<ActionResponses<T>> AddAsync(T entity);

    Task<ActionResponses<T>> DeteleAsync(int id);

    Task<ActionResponses<T>> UpdateAsync(T entity);
}