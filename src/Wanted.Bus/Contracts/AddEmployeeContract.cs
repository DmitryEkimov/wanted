namespace Wanted.Bus.Contracts;

public record AddEmployeeContract(
    string FirstName,
    string LastName,
    string? SurName,
    string EMail,
    string Number
);
