using System;
using System.Collections.Generic;
using System.Linq;
using CSharpFunctionalExtensions;
using FluentValidation;

namespace Api.Extensions
{
    public static class FluentValidationExtensions
    {
        public static IRuleBuilderOptionsConditions<T, IEnumerable<TElement>> AtLeast<T, TElement>(
            this IRuleBuilder<T, IEnumerable<TElement>> builder, int minimum)
        {
            return builder.Custom((enumerable, context) =>
            {
                if (enumerable != null && enumerable.Count() < minimum)
                {
                    context.AddFailure($"The list must contain at least {minimum} but it contains {enumerable.Count()} items");
                }
            });
        }

        public static IRuleBuilderOptionsConditions<T, IEnumerable<TElement>> AtMost<T, TElement>(
            this IRuleBuilder<T, IEnumerable<TElement>> builder, int maximum)
        {
            return builder.Custom((enumerable, context) =>
            {
                if (enumerable != null && enumerable.Count() > maximum)
                {
                    context.AddFailure($"The list must contain at most {maximum} but it contains {enumerable.Count()} items");
                }
            });
        }

        public static IRuleBuilderOptions<T, string> MustBeValueObject<T, TValueObject>(
            this IRuleBuilder<T, string> builder, Func<string, Result<TValueObject>> factory)
        {
            return (IRuleBuilderOptions<T, string>)builder.Custom((value, context) =>
            {
                if (string.IsNullOrEmpty(value))
                    return;

                var result = factory(value);

                if (result.IsFailure)
                    context.AddFailure(result.Error);
            });
        }
    }
}
