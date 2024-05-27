namespace Wanted.Domain.AggregateRoots;

public sealed class Employee(
    Guid id,
    string firstName,
    string lastName,
    string? surName,
    string email,
    string number
) : AggregateRoot<Guid>(id)
{
    public string FirstName { get; } = firstName;

    public string LastName { get; } = lastName;

    public string? SurName { get; } = surName;

    public string Email { get; } = email;

    public string Number { get; } = number;
}
