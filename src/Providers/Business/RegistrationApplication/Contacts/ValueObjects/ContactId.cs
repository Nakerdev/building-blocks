using LanguageExt;
using ValueObjects;

namespace Providers.Business.RegistrationApplication.Contacts.ValueObjects
{
    public sealed class ContactId : ValueObject
    {
        private readonly Guid Value;

        public static Either<ValidationError, ContactId> Create(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return new ValidationError(nameof(ContactId), ValidationErrorCode.Required);
            }

            Guid id;
            if (Guid.TryParse(value, out id))
            {
                return new ValidationError(nameof(ContactId), ValidationErrorCode.InvalidFormat);
            }

            return new ContactId(id);
        }

        private ContactId(Guid value)
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
