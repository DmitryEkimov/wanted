namespace Wanted.Persistence.Entities;

internal class DbEntity<T>(T id)
{
    public T Id { get; private set; } = id;
}
