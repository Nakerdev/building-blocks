using LanguageExt;
using Providers.Business.RegistrationApplication.Contacts.ValueObjects;
using Providers.Business.RegistrationApplication.Contracts.ValueObjects;
using Providers.Business.RegistrationApplication.ValueObjects;
using ValueObjects;

namespace Providers.Application.RegistrationApplication.SignContract
{
    public sealed class SignContractRequest
    {
        public readonly RegistrationApplicationId ProviderRegistrationApplicationId;
        public readonly ContractId ContractId;

        public static Validation<ValidationError, SignContractRequest> Create(RequestDto req)
        {
            //validations and so on
            throw new NotImplementedException();
        }

        private SignContractRequest(
            RegistrationApplicationId providerRegistrationApplicationId,
            ContractId contractId)
        {
            ProviderRegistrationApplicationId = providerRegistrationApplicationId;
            ContractId = contractId;
        }

        public sealed class RequestDto
        {
            public readonly Guid ProviderRegistrationApplicationId;
            public readonly Guid ContractId;
            public RequestDto(
                Guid providerRegistrationApplicationId,
                Guid contractId)
            {
                ProviderRegistrationApplicationId = providerRegistrationApplicationId;
                ContractId = contractId;
            }
        }
    }
}
