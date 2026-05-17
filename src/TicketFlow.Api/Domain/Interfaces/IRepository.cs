using TicketFlow.Api.Domain.Common;

namespace TicketFlow.Api.Domain.Interfaces;

public interface IRepository<T> where T : Entity
{
    T? this[Guid id] { get; }

    Task AddAsync(T item, CancellationToken cancellationToken = default);

    Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IReadOnlyList<T>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken = default);
}
