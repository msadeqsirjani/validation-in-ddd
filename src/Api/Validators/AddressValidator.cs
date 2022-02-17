using FluentValidation;

namespace Api.Validators
{
    public class AddressValidator : AbstractValidator<AddressDto>
    {
        public AddressValidator()
        {
            RuleFor(x => x.Street)
                .NotNull()
                .WithMessage("Street cannot be null")
                .NotEmpty()
                .WithMessage("Street cannot be empty")
                .Length(0, 100)
                .WithMessage("Street is too long");

            RuleFor(x => x.City)
                .NotNull()
                .WithMessage("City cannot be null")
                .NotEmpty()
                .WithMessage("City cannot be empty")
                .Length(0, 40)
                .WithMessage("City is too long");

            RuleFor(x => x.State)
                .NotNull()
                .WithMessage("State cannot be null")
                .NotEmpty()
                .WithMessage("State cannot be empty")
                .Length(0, 2)
                .WithMessage("State is too long");

            RuleFor(x => x.ZipCode)
                .NotNull()
                .WithMessage("ZipCode cannot be null")
                .NotEmpty()
                .WithMessage("ZipCode cannot be empty")
                .Length(0, 5)
                .WithMessage("ZipCode is too long");
        }
    }
}