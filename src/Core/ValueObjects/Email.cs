using LanguageExt;
using System.Net.Mail;

namespace ValueObjects
{
    public sealed class Email : ValueObject
    {
        private readonly string Value;

        public static Either<ValidationError, Email> Create(string value)
        {
            if(string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)) 
            {
                return ValidationError.Required;
            }

            const int MAX_ALLOWED_EMAIL_LENGHT = 255;
            if(value.Length() > MAX_ALLOWED_EMAIL_LENGHT) 
            {
                return ValidationError.MaximumLengthExceeded;
            }

            if (!IsValidEmail(value)) 
            { 
                return ValidationError.InvalidFormat;
            }

            return new Email(value);
        }

        private Email(string value)
        {
            Value = value;
        }

        public override IEnumerable<object> GetEqualityComponents()
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
