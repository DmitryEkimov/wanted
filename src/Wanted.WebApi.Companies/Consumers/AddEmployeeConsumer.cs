namespace Wanted.WebApi.Companies.Consumers;

using AutoMapper;
using Bus.Contracts;
using Commands.AddEmployee;
using MassTransit;
using MediatR;

public sealed class AddEmployeeConsumer(IMapper mapper, ISender mediatr)
    : IConsumer<AddEmployeeContract>
{
    public async Task Consume(ConsumeContext<AddEmployeeContract> context)
    {
        var request = mapper.Map<AddEmployeeRequest>(context.Message);
        await mediatr.Send(request, context.CancellationToken);
    }
}
