using LanguageExt;
using ValueObjects;

namespace Providers.Business.RegistrationApplication.ValueObjects
{
    public sealed class CityId : ValueObject
    {
        public readonly Guid Value;

        public static Either<ValidationError, CityId> Create(string value, string? customErrorFieldId = null)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return BuildValidationError(ValidationErrorCode.MaximumLengthExceeded);
            }

            Guid id;
            if (Guid.TryParse(value, out id))
            {
                return BuildValidationError(ValidationErrorCode.InvalidFormat);
            }

            return new CityId(id);

            ValidationError BuildValidationError(ValidationErrorCode errorCode)
            {
                var fieldId = customErrorFieldId == null ? nameof(CityId) : customErrorFieldId;
                return new ValidationError(fieldId, errorCode);
            }
        }

        public static CityId UnsafeCreate(Guid value) 
        { 
            return new CityId (value);
        }

        private CityId(Guid value)
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
