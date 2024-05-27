namespace Wanted.Services;

using Domain.AggregateRoots;
using ErrorOr;

public interface IWriteRepository<in TEntity, TU>
    where TEntity : AggregateRoot<TU>
{
    Task<ErrorOr<TU>> Save(TEntity entity, CancellationToken cancellationToken);
}
