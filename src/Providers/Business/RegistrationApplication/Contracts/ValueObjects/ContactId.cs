using LanguageExt;
using System.Reflection.Metadata.Ecma335;
using ValueObjects;

namespace Providers.Business.RegistrationApplication.Contracts.ValueObjects
{
    public sealed class ContractId : ValueObject
    {
        public readonly Guid Value;

        public static Either<ValidationError, ContractId> Create(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return new ValidationError(nameof(ContractId), ValidationErrorCode.Required);
            }

            Guid id;
            if (Guid.TryParse(value, out id))
            {
                return new ValidationError(nameof(ContractId), ValidationErrorCode.InvalidFormat);
            }

            return new ContractId(id);
        }

        public static ContractId UnsafeCreate(Guid value) 
        {
            return new ContractId(value);
        }

        private ContractId(Guid value)
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
