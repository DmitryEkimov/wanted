namespace Wanted.Commands.GetCompany;

using ErrorOr;
using MediatR;
using Wanted.Domain.AggregateRoots;
using Wanted.Services;

public sealed class GetCompanyHandler(IReadRepository<Company, Guid> companyRepository)
    : IRequestHandler<GetCompanyRequest, ErrorOr<Company>>
{
    public async Task<ErrorOr<Company>> Handle(
        GetCompanyRequest request,
        CancellationToken cancellationToken
    ) => await companyRepository.GetById(request.Id, cancellationToken);
}
