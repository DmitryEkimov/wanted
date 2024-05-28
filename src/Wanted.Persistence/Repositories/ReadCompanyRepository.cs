namespace Wanted.Persistence.Repositories;

using AutoMapper;
using Domain.AggregateRoots;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Services;

public class ReadCompanyRepository(DatabaseContext context, IMapper mapper)
    : IReadRepository<Company, Guid>
{
    public async Task<ErrorOr<bool>> Exists(Guid id, CancellationToken cancellationToken)
    {
        try
        {
            return await context.Companies.AnyAsync(x => x.Id == id, cancellationToken);
        }
        catch (Exception e)
        {
            return Error.Failure(e.Message);
        }
    }

    public async Task<ErrorOr<Company>> GetById(Guid id, CancellationToken cancellationToken)
    {
        if (!await context.CanConnectAsync())
        {
            return Error.Failure(description: "Database is not accessible");
        }

        if (!context.Database.IsRelational())
        {
            // for InMemory seed
            await context.Database.EnsureCreatedAsync(cancellationToken);
        }
        try
        {
            var isCompanyExists = await this.Exists(id, cancellationToken);
            if (!isCompanyExists.Value)
            {
                return Error.NotFound(description: "Company with this id does not exist");
            }
            var dbEntity = await context
                .Companies.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
            if (dbEntity is null)
            {
                return Error.NotFound("Company with this id does not exist");
            }
            return mapper.Map<Company>(dbEntity);
        }
        catch (Exception e)
        {
            return Error.Failure(e.Message);
        }
    }
}
