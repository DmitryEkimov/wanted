namespace Wanted.Persistence.Repositories;

using AutoMapper;
using ErrorOr;
using Extensions;
using Services;

public class WriteCompanyEmployeeRelationRepository(DatabaseContext context, IMapper mapper)
    : IWriteRelationsRepository<Domain.CompanyEmployeeRelationEntity>
{
    public async Task<ErrorOr<Success>> WriteRelation(
        Domain.CompanyEmployeeRelationEntity entity,
        CancellationToken cancellationToken
    )
    {
        var dbEntity = mapper.Map<Entities.CompanyEmployee>(entity);
        if (!await context.CanConnectAsync())
        {
            return Error.Failure(description: "Database is not accessible");
        }
        context.CompanyEmployees.Add(dbEntity);
        return await context.SaveChangesAsyncExt(cancellationToken);
    }
}
