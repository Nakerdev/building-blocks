using LanguageExt;
using System.Net.Mail;

namespace ValueObjects
{
    public sealed class Email : ValueObject
    {
        public readonly string Value;

        public static Either<ValidationError, Email> Create(string value)
        {
            if(string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)) 
            {
                return new ValidationError(nameof(Email), ValidationErrorCode.Required);
            }

            const int MAX_ALLOWED_EMAIL_LENGHT = 255;
            if(value.Length() > MAX_ALLOWED_EMAIL_LENGHT) 
            {
                return new ValidationError(nameof(Email), ValidationErrorCode.MaximumLengthExceeded);
            }

            if (!IsValidEmail(value)) 
            { 
                return new ValidationError(nameof(Email), ValidationErrorCode.InvalidFormat);
            }

            return new Email(value);
        }

        public static Email UnsafeCreate(string value)
        {
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
