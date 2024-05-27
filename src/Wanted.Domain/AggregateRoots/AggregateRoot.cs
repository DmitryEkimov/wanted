namespace Wanted.Domain.AggregateRoots;

public class AggregateRoot<T>(T id)
{
    public T Id { get; } = id;
}
