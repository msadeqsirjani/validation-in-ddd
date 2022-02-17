using System.Collections.Generic;
using Api.Extensions;
using FluentValidation;

namespace Api.Validators
{
    public class AddressesValidator : AbstractValidator<ICollection<AddressDto>>
    {
        public AddressesValidator()
        {
            RuleFor(x => x)
                .AtLeast(1)
                .AtMost(3)
                .ForEach(x =>
                {
                    x.NotNull()
                        .WithMessage("Address cannot be null");

                    x.SetValidator(new AddressValidator());
                });

        }
    }
}