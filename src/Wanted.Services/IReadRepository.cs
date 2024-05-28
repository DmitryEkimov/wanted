namespace Wanted.Services;

using Domain.AggregateRoots;
using ErrorOr;

public interface IReadRepository<TEntity, in TU>
    where TEntity : AggregateRoot<TU>
{
    Task<ErrorOr<bool>> Exists(TU id, CancellationToken cancellationToken);
    Task<ErrorOr<TEntity>> GetById(TU id, CancellationToken cancellationToken);
}
