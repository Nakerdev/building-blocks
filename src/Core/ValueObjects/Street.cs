using LanguageExt;

namespace ValueObjects
{
    public sealed class Street : ValueObject
    {
        private readonly string Value;

        public static Either<ValidationError, Street> Create(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return ValidationError.Required;
            }

            const int MAX_ALLOWED_STREET_LENGHT = 255;
            if (value.Length() > MAX_ALLOWED_STREET_LENGHT)
            {
                return ValidationError.MaximumLengthExceeded;
            }

            return new Street(value);
        }

        private Street(string value)
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
