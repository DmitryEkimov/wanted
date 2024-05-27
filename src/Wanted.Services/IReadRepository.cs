namespace Wanted.Services;

using Domain.AggregateRoots;

public interface IReadRepository<TEntity, in TU>
    where TEntity : AggregateRoot<TU>
{
    Task<bool> Exists(TU id, CancellationToken cancellationToken);
    Task<TEntity?> GetById(TU id, CancellationToken cancellationToken);
}
