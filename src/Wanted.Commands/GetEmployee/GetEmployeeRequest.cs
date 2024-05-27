namespace Wanted.Commands.GetEmployee;

using ErrorOr;
using MediatR;
using Wanted.Domain.AggregateRoots;

public record GetEmployeeRequest(Guid Id) : IRequest<ErrorOr<Employee>>;
