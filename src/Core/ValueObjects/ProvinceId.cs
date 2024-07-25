using LanguageExt;
using ValueObjects;

namespace Providers.Business.RegistrationApplication.ValueObjects
{
    public sealed class ProvinceId : ValueObject
    {
        public readonly Guid Value;

        public static Either<ValidationError, ProvinceId> Create(string value, string? customErrorFieldId = null)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return BuildValidationError(ValidationErrorCode.Required);
            }

            Guid id;
            if (Guid.TryParse(value, out id))
            {
                return BuildValidationError(ValidationErrorCode.InvalidFormat);
            }

            return new ProvinceId(id);

            ValidationError BuildValidationError(ValidationErrorCode errorCode)
            {
                var fieldId = customErrorFieldId == null ? nameof(ProvinceId) : customErrorFieldId;
                return new ValidationError(fieldId, errorCode);
            }
        }

        public static ProvinceId UnsafeCreate(Guid value) 
        { 
            return new ProvinceId (value);
        }

        private ProvinceId(Guid value)
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
