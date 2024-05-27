namespace Wanted.Persistence.Repositories;

using System.Runtime.InteropServices.JavaScript;
using AutoMapper;
using Domain.AggregateRoots;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using Services;

public class ReadCompanyRepository(DatabaseContext context, IMapper mapper)
    : IReadRepository<Company, Guid>
{
    public Task<bool> Exists(Guid id, CancellationToken cancellationToken) =>
        context.Companies.AnyAsync(x => x.Id == id, cancellationToken);

    public async Task<Company?> GetById(Guid id, CancellationToken cancellationToken)
    {
        if (!await context.CanConnectAsync())
        {
            return null;
        }

        if (!context.Database.IsRelational())
        {
            // for InMemory seed
            await context.Database.EnsureCreatedAsync(cancellationToken);
        }
        var dbEntity = await context
            .Companies.AsNoTracking()
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        if (dbEntity is null)
        {
            return null;
        }

        try
        {
            return mapper.Map<Company>(dbEntity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}
