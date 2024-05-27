namespace Wanted.DomainServicesTests;

using Domain;
using Domain.AggregateRoots;
using FluentAssertions;
using NSubstitute;
using Services;

public class BindEmployeeToCompanyServiceTests
{
    private readonly BindEmployeeToCompanyService service;
    private readonly IReadRepository<Company, Guid> companyRepository;
    private readonly IReadRepository<Employee, Guid> employeeRepository;
    private readonly IWriteRelationsRepository<Domain.CompanyEmployeeRelationEntity> writeRelationRepository;

    public BindEmployeeToCompanyServiceTests()
    {
        this.companyRepository = Substitute.For<IReadRepository<Company, Guid>>();
        this.employeeRepository = Substitute.For<IReadRepository<Employee, Guid>>();
        this.writeRelationRepository = Substitute.For<
            IWriteRelationsRepository<Domain.CompanyEmployeeRelationEntity>
        >();
        this.service = new BindEmployeeToCompanyService(
            this.companyRepository,
            this.employeeRepository,
            this.writeRelationRepository
        );
    }

    [Fact]
    public async Task TestEmployeeNotFound()
    {
        var companyId = Guid.NewGuid();
        this.companyRepository.Exists(companyId, Arg.Any<CancellationToken>()).Returns(true);
        this.employeeRepository.Exists(Arg.Any<Guid>(), Arg.Any<CancellationToken>())
            .Returns(false);
        var result = await this.service.BindEmployeeToCompany(
            new CompanyEmployeeRelationEntity(companyId, Guid.NewGuid()),
            CancellationToken.None
        );
        result.IsError.Should().BeTrue();
    }

    [Fact]
    public async Task TestCompanyNotFound()
    {
        this.companyRepository.Exists(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(false);
        var employeeId = Guid.NewGuid();
        this.employeeRepository.Exists(employeeId, Arg.Any<CancellationToken>()).Returns(true);
        var result = await this.service.BindEmployeeToCompany(
            new CompanyEmployeeRelationEntity(Guid.NewGuid(), employeeId),
            CancellationToken.None
        );
        result.IsError.Should().BeTrue();
    }

    [Fact]
    public async Task TestCompanyAndEmployeeFounded()
    {
        var companyId = Guid.NewGuid();
        this.companyRepository.Exists(companyId, Arg.Any<CancellationToken>()).Returns(true);
        var employeeId = Guid.NewGuid();
        this.employeeRepository.Exists(employeeId, Arg.Any<CancellationToken>()).Returns(true);
        var relation = new CompanyEmployeeRelationEntity(companyId, employeeId);
        var result = await this.service.BindEmployeeToCompany(relation, CancellationToken.None);
        result.IsError.Should().BeFalse();
        await this
            .writeRelationRepository.Received()
            .WriteRelation(relation, CancellationToken.None);
    }
}
