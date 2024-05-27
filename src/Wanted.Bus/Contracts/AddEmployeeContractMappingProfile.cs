namespace Wanted.Bus.Contracts;

using AutoMapper;
using Commands.AddEmployee;

public class AddEmployeeContractMappingProfile : Profile
{
    public AddEmployeeContractMappingProfile() =>
        this.CreateMap<AddEmployeeContract, AddEmployeeRequest>();
}
