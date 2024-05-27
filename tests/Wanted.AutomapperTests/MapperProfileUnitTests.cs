namespace Wanted.AutomapperTests;

using AutoMapper;
using Commands.AddEmployee;
using Commands.BindEmployeeToCompany;
using Persistence.MappingProfiles;

public class MapperProfileUnitTests
{
    [Fact]
    public void AddEmployeeRequestMappingProfileIsValid()
    {
        var config = new MapperConfiguration(cfg =>
            cfg.AddProfile<AddEmployeeRequestMappingProfile>()
        );
        config.AssertConfigurationIsValid();
    }

    [Fact]
    public void BindEmployeeToCompanyRequestMappingProfileIsValid()
    {
        var config = new MapperConfiguration(cfg =>
            cfg.AddProfile<BindEmployeeToCompanyRequestMappingProfile>()
        );
        config.AssertConfigurationIsValid();
    }

    [Fact]
    public void CompanyEmployeeMappingProfileIsValid()
    {
        var config = new MapperConfiguration(cfg =>
            cfg.AddProfile<CompanyEmployeeMappingProfile>()
        );
        config.AssertConfigurationIsValid();
    }

    [Fact]
    public void CompanyMappingProfileIsValid()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<CompanyMappingProfile>());
        config.AssertConfigurationIsValid();
    }

    [Fact]
    public void EmployeeMappingProfileIsValid()
    {
        var config = new MapperConfiguration(cfg => cfg.AddProfile<EmployeeMappingProfile>());
        config.AssertConfigurationIsValid();
    }
}
