namespace Wanted.Persistence.MappingProfiles;

using AutoMapper;

public class CompanyMappingProfile : Profile
{
    public CompanyMappingProfile() =>
        this.CreateMap<Wanted.Persistence.Entities.Company, Domain.AggregateRoots.Company>()
            .ReverseMap()
            .ForMember(x => x.Employees, o => o.Ignore());
}
