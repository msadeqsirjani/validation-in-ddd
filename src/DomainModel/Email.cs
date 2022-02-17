using System.Collections.Generic;
using System.Text.RegularExpressions;
using CSharpFunctionalExtensions;

namespace DomainModel
{
    public class Email : ValueObject
    {
        public string Value { get; }

        private Email(string value)
        {
            Value = value;
        }

        public static Result<Email> Initial(string value)
        {
            if (string.IsNullOrEmpty(value))
                return Result.Failure<Email>("Value is null or empty");

            var email = value.Trim();

            if (email.Length > 150)
                return Result.Failure<Email>("Value is too long");

            if (!Regex.IsMatch(email, @"^(.+)@(.+)$"))
                return Result.Failure<Email>("Value is invalid");

            return Result.Success(new Email(email));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}
