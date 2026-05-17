using TicketFlow.Api.Domain.Common;
using TicketFlow.Api.Domain.Interfaces;

namespace TicketFlow.Api.Application.Repositories;

public sealed class InMemoryRepository<T> : IRepository<T> where T : Entity
{
    private readonly Dictionary<Guid, T> _items = new();

    public T? this[Guid id] => _items.TryGetValue(id, out var item) ? item : null;

    public Task AddAsync(T item, CancellationToken cancellationToken = default)
    {
        _items[item.Id] = item;
        return Task.CompletedTask;
    }

    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(this[id]);
    }

    public Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        IReadOnlyList<T> result = _items.Values.ToList();
        return Task.FromResult(result);
    }

    public Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(_items.Remove(id));
    }
}
