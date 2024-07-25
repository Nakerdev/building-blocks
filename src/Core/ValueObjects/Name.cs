using LanguageExt;

namespace ValueObjects
{
    public sealed class Name : ValueObject
    {
        public readonly string Value;

        public static Either<ValidationError, Name> Create(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return new ValidationError(nameof(Name), ValidationErrorCode.Required);
            }

            const int MAX_ALLOWED_NAME_LENGHT = 255;
            if (value.Length() > MAX_ALLOWED_NAME_LENGHT)
            {
                return new ValidationError(nameof(Name), ValidationErrorCode.MaximumLengthExceeded);
            }

            return new Name(value);
        }

        public static Name UnsafeCreate(string value)
        {
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
