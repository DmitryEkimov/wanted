namespace Wanted.Persistence.MappingProfiles;

using AutoMapper;

public class CompanyEmployeeMappingProfile : Profile
{
    public CompanyEmployeeMappingProfile() =>
        this.CreateMap<
            Domain.CompanyEmployeeRelationEntity,
            Wanted.Persistence.Entities.CompanyEmployee
        >()
            .ReverseMap();
}
