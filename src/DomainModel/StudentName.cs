using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace DomainModel
{
    public class StudentName : ValueObject
    {
        public string Value { get; }

        private StudentName(string value)
        {
            Value = value;
        }

        public static Result<StudentName> Initial(string value)
        {
            if (string.IsNullOrEmpty(value))
                return Result.Failure<StudentName>("Value is null or empty");

            var email = value.Trim();

            if (email.Length > 200)
                return Result.Failure<StudentName>("Value is too long");

            return Result.Success(new StudentName(email));
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }
    }
}