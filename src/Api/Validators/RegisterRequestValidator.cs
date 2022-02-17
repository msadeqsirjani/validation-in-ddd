using Api.Extensions;
using DomainModel;
using FluentValidation;

namespace Api.Validators
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .MustBeValueObject(StudentName.Initial)
                .When(x=>x.Name != null, ApplyConditionTo.CurrentValidator);

            When(x => x.Email == null, () =>
            {
                RuleFor(x => x.Phone)
                    .NotNull()
                    .WithMessage("Phone cannot be null")
                    .NotEmpty()
                    .WithMessage("Phone cannot be empty");
            });

            When(x => x.Phone == null, () =>
            {
                RuleFor(x => x.Email)
                    .NotNull()
                    .WithMessage("Email cannot be null")
                    .NotEmpty()
                    .WithMessage("Email cannot be empty");
            });

            RuleFor(x => x.Email)
                .NotNull()
                .MustBeValueObject(Email.Initial)
                .When(x => x.Email != null, ApplyConditionTo.CurrentValidator);

            RuleFor(x => x.Phone)
                .NotNull()
                .WithMessage("Phone cannot be null")
                .NotEmpty()
                .WithMessage("Phone cannot be empty")
                .Matches(@"^[ 2-9][0-9]{9}$")
                .WithMessage("Phone is invalid");

            RuleFor(x => x.Addresses)
                .NotNull()
                .WithMessage("Addresses can not be null")
                .SetValidator(new AddressesValidator())
                .When(x => true, ApplyConditionTo.CurrentValidator);
        }
    }
}
