using LanguageExt;

namespace ValueObjects
{
    public sealed class PostalCode : ValueObject
    {
        private readonly int Value;

        public static Either<ValidationError, PostalCode> Create(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return ValidationError.Required;
            }

            const int MAX_ALLOWED_SPANISH_POSTAL_CODE_LENGTH = 5;
            if (value.Length() > MAX_ALLOWED_SPANISH_POSTAL_CODE_LENGTH)
            {
                return ValidationError.MaximumLengthExceeded;
            }

            int postalCode;
            if (int.TryParse(value, out postalCode))
            {
                return ValidationError.InvalidFormat;
            }

            return new PostalCode(postalCode);
        }

        private PostalCode(int value)
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
    }
}
