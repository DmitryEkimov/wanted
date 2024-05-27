namespace Wanted.Persistence.Repositories;

using AutoMapper;
using Domain.AggregateRoots;
using ErrorOr;
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
        var isSuccess = await context.SaveChangesAsync(cancellationToken) > 0;
        return isSuccess ? entity.Id : Error.Failure("employee is not saved");
    }
}
