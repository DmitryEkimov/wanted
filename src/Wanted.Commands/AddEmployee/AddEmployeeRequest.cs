namespace Wanted.Commands.AddEmployee;

using ErrorOr;
using MediatR;

public record AddEmployeeRequest(
    string FirstName,
    string LastName,
    string? SurName,
    string EMail,
    string Number
) : IRequest<ErrorOr<Guid>>;
