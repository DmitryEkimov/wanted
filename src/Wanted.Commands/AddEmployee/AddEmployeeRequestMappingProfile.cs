namespace Wanted.Commands.AddEmployee;

using AutoMapper;
using Wanted.Domain.AggregateRoots;

public class AddEmployeeRequestMappingProfile : Profile
{
    public AddEmployeeRequestMappingProfile() =>
        this.CreateMap<AddEmployeeRequest, Employee>()
            .ConstructUsing(x => new Employee(
                Guid.NewGuid(),
                x.FirstName,
                x.LastName,
                x.SurName,
                x.EMail,
                x.Number
            ));
}
