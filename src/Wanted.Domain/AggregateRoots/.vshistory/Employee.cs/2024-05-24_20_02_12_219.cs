namespace Wanted.Entities;

public sealed class Employee
{
    public Employee(Guid id, string firstName, string lastName, string email, Company? company)
    {
        this.Id = id;
        this.FirstName = firstName;
        this.LastName = lastName;
        this.Email = email;
        this.LastName = lastName;
        this.Company = company;
    }

    private Employee()
    {
// for EF
    }

    public Guid Id { get; private set; }

    public string FirstName { get; private set; } = default!;

    public string LastName { get; private set; } = default!;

    public string Email { get; private set; } = default!;

    public Company? Company { get; private set; }// Company
}
