namespace Wanted.Persistence.Repositories;

using AutoMapper;
using Domain.AggregateRoots;
using ErrorOr;
using Extensions;
using Services;

public class WriteEmployeeRepository(DatabaseContext context, IMapper mapper)
    : IWriteRepository<Employee, Guid>
{
    public async Task<ErrorOr<Guid>> Save(Employee entity, CancellationToken cancellationToken)
    {
        var dbEntity = mapper.Map<Wanted.Persistence.Entities.Employee>(entity);
        if (!await context.CanConnectAsync())
        {
            return Error.Failure(description: "Database is not accessible");
        }
        context.Employees.Add(dbEntity);
        var result = await context.SaveChangesAsyncExt(cancellationToken);
        return result.IsError ? ErrorOr<Guid>.From(result.Errors) : entity.Id;
    }
}
