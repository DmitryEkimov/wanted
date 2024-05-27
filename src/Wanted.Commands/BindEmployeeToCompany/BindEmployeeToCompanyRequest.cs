namespace Wanted.Commands.BindEmployeeToCompany;

using ErrorOr;
using MediatR;

public record BindEmployeeToCompanyRequest(Guid CompanyId, Guid EmployeeId)
    : IRequest<ErrorOr<Success>>;
