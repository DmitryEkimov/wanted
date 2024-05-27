namespace Wanted.Domain;

public sealed class CompanyEmployeeRelationEntity(Guid companyId, Guid employeeId)
{
    public Guid CompanyId { get; } = companyId;

    public Guid EmployeeId { get; } = employeeId;
}
