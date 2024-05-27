namespace Wanted.Services;

using Domain;
using Domain.AggregateRoots;
using ErrorOr;

public sealed class BindEmployeeToCompanyService(
    IReadRepository<Company, Guid> companyRepository,
    IReadRepository<Employee, Guid> employeeRepository,
    IWriteRelationsRepository<Domain.CompanyEmployeeRelationEntity> writeRelationRepository
) : IBindEmployeeToCompanyService
{
    public async Task<ErrorOr<Success>> BindEmployeeToCompany(
        CompanyEmployeeRelationEntity relation,
        CancellationToken cancellationToken
    )
    {
        List<Error> errors = [];
        if (!await companyRepository.Exists(relation.CompanyId, cancellationToken))
        {
            errors.Add(Error.NotFound(description: "Company with this id does not exist"));
        }

        if (!await employeeRepository.Exists(relation.EmployeeId, cancellationToken))
        {
            errors.Add(Error.NotFound(description: "Employee with this id does not exist"));
        }

        if (errors.Count > 0)
        {
            return errors;
        }

        return await writeRelationRepository.WriteRelation(relation, cancellationToken);
    }
}
