using LanguageExt;

namespace ValueObjects
{
    public sealed class PostalCode : ValueObject
    {
        private readonly int Value;

        public static Either<ValidationError, PostalCode> Create(string value, string? customErrorFieldId = null)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return BuildValidationError(ValidationErrorCode.Required);
            }

            const int MAX_ALLOWED_SPANISH_POSTAL_CODE_LENGTH = 5;
            if (value.Length() > MAX_ALLOWED_SPANISH_POSTAL_CODE_LENGTH)
            {
                return BuildValidationError(ValidationErrorCode.MaximumLengthExceeded);
            }

            int postalCode;
            if (int.TryParse(value, out postalCode))
            {
                return BuildValidationError(ValidationErrorCode.InvalidFormat);
            }

            return new PostalCode(postalCode);

            ValidationError BuildValidationError(ValidationErrorCode errorCode)
            {
                var fieldId = customErrorFieldId == null ? nameof(PostalCode) : customErrorFieldId;
                return new ValidationError(fieldId, errorCode);
            }
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
