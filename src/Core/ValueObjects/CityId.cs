using LanguageExt;
using ValueObjects;

namespace Providers.Business.RegistrationApplication.ValueObjects
{
    public sealed class CityId : ValueObject
    {
        private readonly Guid Value;

        public static Either<ValidationError, CityId> Create(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return ValidationError.Required;
            }

            Guid id;
            if (Guid.TryParse(value, out id))
            {
                return ValidationError.InvalidFormat;
            }

            return new CityId(id);
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
