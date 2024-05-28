namespace Wanted.Commands.GetEmployee;

using ErrorOr;
using MediatR;
using Wanted.Domain.AggregateRoots;
using Wanted.Services;

public sealed class GetEmployeeHandler(IReadRepository<Employee, Guid> employeeRepository)
    : IRequestHandler<GetEmployeeRequest, ErrorOr<Employee>>
{
    public Task<ErrorOr<Employee>> Handle(
        GetEmployeeRequest request,
        CancellationToken cancellationToken
    ) => employeeRepository.GetById(request.Id, cancellationToken);
}
