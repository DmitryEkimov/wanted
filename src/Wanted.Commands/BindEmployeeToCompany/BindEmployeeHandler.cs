namespace Wanted.Commands.BindEmployeeToCompany;

using AutoMapper;
using ErrorOr;
using MediatR;
using Wanted.Domain;
using Wanted.Services;

public class BindEmployeeHandler(IMapper mapper, IBindEmployeeToCompanyService srv)
    : IRequestHandler<BindEmployeeToCompanyRequest, ErrorOr<Success>>
{
    public async Task<ErrorOr<Success>> Handle(
        BindEmployeeToCompanyRequest request,
        CancellationToken cancellationToken
    ) =>
        await srv.BindEmployeeToCompany(
            mapper.Map<CompanyEmployeeRelationEntity>(request),
            cancellationToken
        );
}
