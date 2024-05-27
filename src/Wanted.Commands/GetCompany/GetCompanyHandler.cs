namespace Wanted.Commands.GetCompany;

using AutoMapper;
using ErrorOr;
using MediatR;
using Wanted.Domain.AggregateRoots;
using Wanted.Services;

public sealed class GetCompanyHandler(
    IMapper mapper,
    IReadRepository<Company, Guid> companyRepository
) : IRequestHandler<GetCompanyRequest, ErrorOr<Company>>
{
    public async Task<ErrorOr<Company>> Handle(
        GetCompanyRequest request,
        CancellationToken cancellationToken
    )
    {
        var dbEntity = await companyRepository.GetById(request.Id, cancellationToken);
        if (dbEntity is null)
        {
            return Error.NotFound(description: "Company not found");
        }
        var domainEntity = mapper.Map<Company>(dbEntity);
        return domainEntity;
    }
}
