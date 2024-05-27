namespace Wanted.Bus.Contracts;

using FluentValidation;

public sealed class AddEmployeeContractValidation : AbstractValidator<AddEmployeeContract>
{
    public AddEmployeeContractValidation()
    {
        this.RuleFor(x => x.FirstName).NotEmpty().WithMessage("FirstName is required");
        this.RuleFor(x => x.LastName).NotEmpty().WithMessage("LastName is required");
        this.RuleFor(x => x.EMail)
            .NotEmpty()
            .WithMessage("Email is required")
            .EmailAddress()
            .WithMessage("Wrong Email address");
        this.RuleFor(x => x.Number).NotEmpty().WithMessage("Number is required");
    }
}
