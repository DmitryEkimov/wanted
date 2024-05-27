namespace Wanted.Persistence.Entities;

internal sealed class Employee(
    Guid id,
    string firstName,
    string lastName,
    string? surName,
    string email,
    string number
) : DbEntity<Guid>(id)
{
    public string FirstName { get; private set; } = firstName;

    public string LastName { get; private set; } = lastName;

    public string? SurName { get; private set; } = surName;

    public string Email { get; private set; } = email;

    public string Number { get; private set; } = number;

    public ISet<Company> Companies { get; set; } = new HashSet<Company>();
}
