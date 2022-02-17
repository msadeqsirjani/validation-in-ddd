using FluentValidation;

namespace Api.Validators
{
    public class EditPersonalInfoRequestValidator : AbstractValidator<EditPersonalInfoRequest>
    {
        public EditPersonalInfoRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .WithMessage("Name cannot be null")
                .NotEmpty()
                .WithMessage("Name cannot be empty")
                .Length(0, 200)
                .WithMessage("Name is too long");

            RuleFor(x => x.Addresses)
                .NotNull()
                .WithMessage("Addresses can not be null")
                .SetValidator(new AddressesValidator());
        }
    }
}