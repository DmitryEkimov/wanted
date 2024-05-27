namespace Wanted.Persistence.Repositories;

using AutoMapper;
using ErrorOr;
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
        var isSuccess = await context.SaveChangesAsync(cancellationToken) > 0;
        return isSuccess ? Result.Success : Error.Failure("relation is not saved");
    }
}
