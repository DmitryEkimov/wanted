namespace Wanted.Persistence.Entities;

internal sealed class Company(Guid id, string name) : DbEntity<Guid>(id)
{
    public string Name { get; private set; } = name;

    public ISet<Employee> Employees { get; set; } = new HashSet<Employee>();
}
