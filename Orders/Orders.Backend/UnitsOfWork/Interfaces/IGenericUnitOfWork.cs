using Orders.shared.Responses;

namespace Orders.Backend.UnitsOfWork.Interfaces;

public interface IGenericUnitOfWork<T> where T : class
{
    Task<ActionResponses<IEnumerable<T>>> GetAsync();

    Task<ActionResponses<T>> AddAsync(T entity);

    Task<ActionResponses<T>> DeteleAsync(int id);

    Task<ActionResponses<T>> UpdateAsync(T entity);

    Task<ActionResponses<T>> GetAsync(int id);
}