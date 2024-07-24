using LanguageExt;

namespace ValueObjects
{
    public sealed class Name : ValueObject
    {
        private readonly string Value;

        public static Either<ValidationError, Name> Create(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return ValidationError.Required;
            }

            const int MAX_ALLOWED_NAME_LENGHT = 255;
            if (value.Length() > MAX_ALLOWED_NAME_LENGHT)
            {
                return ValidationError.MaximumLengthExceeded;
            }

            return new Name(value);
        }

        private Name(string value)
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
