namespace Wanted.Services;

using Domain;
using ErrorOr;

public interface IBindEmployeeToCompanyService
{
    Task<ErrorOr<Success>> BindEmployeeToCompany(
        CompanyEmployeeRelationEntity relation,
        CancellationToken cancellationToken
    );
}
