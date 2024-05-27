namespace Wanted.Domain.AggregateRoots;

public sealed class Company(Guid id, string name) : AggregateRoot<Guid>(id)
{
    public string Name { get; } = name;
}
