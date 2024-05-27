namespace Wanted.Persistence.MappingProfiles;

using AutoMapper;

public class EmployeeMappingProfile : Profile
{
    public EmployeeMappingProfile() =>
        this.CreateMap<Domain.AggregateRoots.Employee, Wanted.Persistence.Entities.Employee>()
            .ForMember(x => x.Companies, o => o.Ignore())
            .ReverseMap();
}
