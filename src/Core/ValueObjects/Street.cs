using LanguageExt;

namespace ValueObjects
{
    public sealed class Street : ValueObject
    {
        public readonly string Value;

        public static Either<ValidationError, Street> Create(string value, string? customErrorFieldId = null)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return BuildValidationError(ValidationErrorCode.Required);
            }

            const int MAX_ALLOWED_STREET_LENGHT = 255;
            if (value.Length() > MAX_ALLOWED_STREET_LENGHT)
            {
                return BuildValidationError(ValidationErrorCode.MaximumLengthExceeded);
            }

            return new Street(value);

            ValidationError BuildValidationError(ValidationErrorCode errorCode)
            {
                var fieldId = customErrorFieldId == null ? nameof(Street) : customErrorFieldId;
                return new ValidationError(fieldId, errorCode);
            }
        }

        public static Street UnsafeCreate(string value) 
        { 
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
