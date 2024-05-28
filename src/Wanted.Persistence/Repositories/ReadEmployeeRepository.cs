namespace Wanted.Persistence.Repositories;

using AutoMapper;
using Domain.AggregateRoots;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Services;

public class ReadEmployeeRepository(DatabaseContext context, IMapper mapper)
    : IReadRepository<Employee, Guid>
{
    public async Task<ErrorOr<bool>> Exists(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            return await context.Employees.AnyAsync(x => x.Id == id, cancellationToken);
        }
        catch (Exception e)
        {
            return Error.Failure(e.Message);
        }
    }

    public async Task<ErrorOr<Employee>> GetById(Guid id, CancellationToken cancellationToken)
    {
        if (!await context.CanConnectAsync())
        {
            return Error.Failure(description: "Database is not accessible");
        }
        try
        {
            var isEmployeeExists = await this.Exists(id, cancellationToken);
            if (!isEmployeeExists.Value)
            {
                return Error.NotFound(description: "Employee with this id does not exist");
            }
            var dbEntity = await context
                .Employees.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (dbEntity is null)
            {
                return Error.NotFound(description: "Employee with this id does not exist");
            }
            return mapper.Map<Employee>(dbEntity);
        }
        catch (Exception e)
        {
            return Error.Failure(e.Message);
        }
    }
}
