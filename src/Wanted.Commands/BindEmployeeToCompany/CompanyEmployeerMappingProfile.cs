namespace Wanted.Commands.BindEmployeeToCompany;

using AutoMapper;
using Wanted.Domain;

public class BindEmployeeToCompanyRequestMappingProfile : Profile
{
    public BindEmployeeToCompanyRequestMappingProfile() =>
        this.CreateMap<BindEmployeeToCompanyRequest, CompanyEmployeeRelationEntity>().ReverseMap();
}
