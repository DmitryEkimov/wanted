namespace Wanted.Entities;

public sealed class Company
{
    public Company(Guid id, string name)
    {
        this.Id = id;
        this.Name = name;
    }

    private Company()
    {
// for EF
    }

    public Guid Id { get; private set; }

    public string Name { get; private set; } = default!;
}
