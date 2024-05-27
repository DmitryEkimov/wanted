namespace Wanted.ValidationTests;

using Bus.Contracts;
using Commands.AddEmployee;
using FluentValidation.TestHelper;

public class AddEmployeeRequestValidationTest
{
    private readonly AddEmployeeContractValidation validator = new();

    [Fact]
    public void ShouldHaveErrorWhenFirstNameIsEmpty()
    {
        var model = new AddEmployeeContract(
            string.Empty,
            "blabla",
            "blabla",
            "blabla@bla.com",
            "blabla"
        );
        var result = this.validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(person => person.FirstName);
    }

    [Fact]
    public void ShouldHaveErrorWhenLastNameIsEmpty()
    {
        var model = new AddEmployeeContract(
            "blabla",
            string.Empty,
            "blabla",
            "blabla@bla.com",
            "blabla"
        );
        var result = this.validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(person => person.LastName);
    }

    [Fact]
    public void ShouldHaveErrorWhenEmailIsEmpty()
    {
        var model = new AddEmployeeContract(
            "blabla",
            "blabla",
            "blabla@bla.com",
            string.Empty,
            "blabla"
        );
        var result = this.validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(person => person.EMail);
    }

    [Fact]
    public void ShouldHaveErrorWhenEmailIsWrong()
    {
        var model = new AddEmployeeContract("blabla", "blabla", "blabla", "blabla", "blabla");
        var result = this.validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(person => person.EMail);
    }

    [Fact]
    public void ShouldHaveErrorWhenNumberIsEmpty()
    {
        var model = new AddEmployeeContract(
            "blabla",
            "blabla",
            "blabla",
            "blabla@bla.com",
            string.Empty
        );
        var result = this.validator.TestValidate(model);
        result.ShouldHaveValidationErrorFor(person => person.Number);
    }

    [Fact]
    public void ShouldNotHaveErrors()
    {
        var model = new AddEmployeeContract(
            "blabla",
            "blabla",
            "blabla",
            "blabla@bla.com",
            "blabla"
        );
        var result = this.validator.TestValidate(model);
        result.ShouldNotHaveAnyValidationErrors();
    }
}
