using Orders.Backend.Repositories.Interfaces;
using Orders.Backend.UnitsOfWork.Interfaces;
using Orders.shared.Responses;

namespace Orders.Backend.UnitsOfWork.Implementations;

public class GenericUnitOfWork<T> : IGenericUnitOfWork<T> where T : class
{
    private readonly IGenericRepository<T> _repository;

    public GenericUnitOfWork(IGenericRepository<T> repository)
    {
        _repository = repository;
    }

    public virtual async Task<ActionResponses<T>> AddAsync(T model) => await _repository.AddAsync(model);

    public virtual async Task<ActionResponses<T>> DeteleAsync(int id) => await _repository.DeteleAsync(id);

    public virtual async Task<ActionResponses<IEnumerable<T>>> GetAsync() => await _repository.GetAsync();

    public virtual async Task<ActionResponses<T>> GetAsync(int id) => await _repository.GetAsync(id);

    public virtual async Task<ActionResponses<T>> UpdateAsync(T model) => await _repository.UpdateAsync(model);
}