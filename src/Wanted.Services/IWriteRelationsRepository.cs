namespace Wanted.Services;

using ErrorOr;

public interface IWriteRelationsRepository<in TRelationEntity>
{
    Task<ErrorOr<Success>> WriteRelation(
        TRelationEntity entity,
        CancellationToken cancellationToken
    );
}
