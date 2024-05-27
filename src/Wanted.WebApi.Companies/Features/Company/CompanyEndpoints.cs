namespace Wanted.WebApi.Companies.Features.Company;

using System.Net;
using Commands.BindEmployeeToCompany;
using Commands.GetCompany;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Wanted.Domain.AggregateRoots;

public static class CompanyEndpoints
{
    private const string EmployeeRoute = "/api/v1/company";

    public static IEndpointRouteBuilder MapCompanyEmployeeEndpoints(
        this IEndpointRouteBuilder endpoints
    )
    {
        var routeGroup = endpoints.MapGroup(EmployeeRoute);
        routeGroup
            .MapPost(
                "{id:guid}/addemployee",
                async (
                    Guid id,
                    [FromBody] BindEmployeeRequest employee,
                    ISender mediatr,
                    CancellationToken cancellationToken
                ) =>
                {
                    var result = await mediatr.Send(
                        new BindEmployeeToCompanyRequest(id, employee.EmployeeId),
                        cancellationToken
                    );
                    if (result.IsError)
                    {
                        return result.FirstError.Code == "General.NotFound"
                            ? Results.NotFound()
                            : Results.Problem(
                                string.Join(",", result.Errors.Select(x => x.Description))
                            );
                    }
                    return Results.Ok();
                }
            )
            .ProducesProblem((int)HttpStatusCode.NotFound)
            .Produces((int)HttpStatusCode.OK);
        routeGroup
            .MapGet(
                "{id:guid}",
                async (Guid id, ISender mediatr, CancellationToken cancellationToken) =>
                {
                    var companyResult = await mediatr.Send(
                        new GetCompanyRequest(id),
                        cancellationToken
                    );
                    if (companyResult.IsError)
                    {
                        return companyResult.FirstError.Code == "General.NotFound"
                            ? Results.NotFound(id)
                            : Results.Problem(
                                string.Join(",", companyResult.Errors.Select(x => x.Description))
                            );
                    }
                    return Results.Ok(companyResult.Value);
                }
            )
            .Produces<Company>()
            .ProducesProblem((int)HttpStatusCode.NotFound);
        return endpoints;
    }

    internal sealed record BindEmployeeRequest(Guid EmployeeId);
}
