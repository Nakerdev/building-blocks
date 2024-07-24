using LanguageExt;
using System.Net.Mail;
using ValueObjects;

namespace Providers.Business.RegistrationApplication.ValueObjects
{
    public sealed class RegistrationApplicationId : ValueObject
    {
        private readonly string Value;

        public static Either<ValidationError, RegistrationApplicationId> Create(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return ValidationError.Required;
            }

            const int MAX_ALLOWED_EMAIL_LENGHT = 255;
            if (value.Length() > MAX_ALLOWED_EMAIL_LENGHT)
            {
                return ValidationError.MaximumLengthExceeded;
            }

            if (!IsValidEmail(value))
            {
                return ValidationError.InvalidFormat;
            }

            return new RegistrationApplicationId(value);
        }

        private RegistrationApplicationId(string value)
        {
            Value = value;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value;
        }

        private static bool IsValidEmail(string email)
        {
            try
            {
                var mailAddress = new MailAddress(email);
            }
            catch
            {
                return false;
            }
            return true;
        }
    }
}
