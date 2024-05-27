namespace Wanted.Commands.GetCompany;

using ErrorOr;
using MediatR;
using Wanted.Domain.AggregateRoots;

public record GetCompanyRequest(Guid Id) : IRequest<ErrorOr<Company>>;
