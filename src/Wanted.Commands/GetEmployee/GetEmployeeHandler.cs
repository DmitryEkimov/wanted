namespace Wanted.Commands.GetEmployee;

using AutoMapper;
using ErrorOr;
using MediatR;
using Wanted.Domain.AggregateRoots;
using Wanted.Services;

public sealed class GetEmployeeHandler(
    IMapper mapper,
    IReadRepository<Employee, Guid> employeeRepository
) : IRequestHandler<GetEmployeeRequest, ErrorOr<Employee>>
{
    public async Task<ErrorOr<Employee>> Handle(
        GetEmployeeRequest request,
        CancellationToken cancellationToken
    )
    {
        var dbEntity = await employeeRepository.GetById(request.Id, cancellationToken);
        if (dbEntity is null)
        {
            return Error.NotFound(description: "Employee not found");
        }
        var domainEntity = mapper.Map<Employee>(dbEntity);
        return domainEntity;
    }
}
