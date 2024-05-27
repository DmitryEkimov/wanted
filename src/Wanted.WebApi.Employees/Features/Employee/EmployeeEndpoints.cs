namespace Wanted.WebApi.Employees.Features.Employee;

using System.Net;
using Bus.Contracts;
using MassTransit;
using MinimalApis.FluentValidation;

public static class EmployeeEndpoints
{
    private const string EmployeeRoute = "/api/v1/employees";

    public static IEndpointRouteBuilder MapEmployeeEndpoints(this IEndpointRouteBuilder endpoints)
    {
        var routeGroup = endpoints.MapGroup(EmployeeRoute);
        routeGroup
            .MapPost(
                string.Empty,
                async (
                    AddEmployeeContract addEmployeeRequest,
                    IPublishEndpoint publishEndpoint,
                    CancellationToken cancellationToken
                ) =>
                {
                    await publishEndpoint.Publish(addEmployeeRequest, cancellationToken);
                    return Results.Ok();
                }
            )
            .Validate<AddEmployeeContract>()
            .ProducesValidationProblem()
            .Produces((int)HttpStatusCode.OK);
        return endpoints;
    }
}
