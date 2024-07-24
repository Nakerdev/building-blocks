using LanguageExt;
using System.Text.RegularExpressions;

namespace ValueObjects
{
    public sealed class PhoneNumber : ValueObject
    {
        private readonly string Value;

        public static Either<ValidationError, PhoneNumber> Create(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return ValidationError.Required;
            }

            if (!IsValidPhoneNumber(value))
            {
                return ValidationError.InvalidFormat;
            }

            return new PhoneNumber(value);
        }

        private PhoneNumber(string value)
        {
            Value = value;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        public static bool IsValidPhoneNumber(string value)
        {
            return Regex.Match(value, @"^(\+[0-9]{9})$").Success;
        }
    }
}
