namespace Wanted.Persistence.Repositories;

using AutoMapper;
using Domain.AggregateRoots;
using Microsoft.EntityFrameworkCore;
using Services;

public class ReadEmployeeRepository(DatabaseContext context, IMapper mapper)
    : IReadRepository<Employee, Guid>
{
    public Task<bool> Exists(Guid id, CancellationToken cancellationToken) =>
        context.Employees.AnyAsync(x => x.Id == id, cancellationToken);

    public async Task<Employee?> GetById(Guid id, CancellationToken cancellationToken)
    {
        if (!await context.CanConnectAsync())
        {
            return null;
        }
        var dbEntity = await context
            .Employees.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (dbEntity is null)
        {
            return null;
        }
        return mapper.Map<Employee>(dbEntity);
    }
}
