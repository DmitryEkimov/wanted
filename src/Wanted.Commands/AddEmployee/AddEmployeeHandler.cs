namespace Wanted.Commands.AddEmployee;

using AutoMapper;
using ErrorOr;
using MediatR;
using Wanted.Domain.AggregateRoots;
using Wanted.Services;

public sealed class AddEmployeeHandler(
    IMapper mapper,
    IWriteRepository<Employee, Guid> employeeRepository
) : IRequestHandler<AddEmployeeRequest, ErrorOr<Guid>>
{
    public async Task<ErrorOr<Guid>> Handle(
        AddEmployeeRequest request,
        CancellationToken cancellationToken
    ) => await employeeRepository.Save(mapper.Map<Employee>(request), cancellationToken);
}
