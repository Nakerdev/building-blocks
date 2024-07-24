using LanguageExt;

namespace Providers.Business.ValueObjects
{
    public sealed class Email : ValueObject
    {
        private readonly string Value;

        public static Either<ValidationError, Email> Create(string value)
        {
            return new Email(value);
        }

        private Email(string value)
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
    }
}
