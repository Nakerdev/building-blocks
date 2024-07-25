using LanguageExt;
using Providers.Business.RegistrationApplication.Contacts.ValueObjects;
using Providers.Business.RegistrationApplication.ValueObjects;
using ValueObjects;

namespace Providers.Application.RegistrationApplication.AddContact
{
    public sealed class AddContactRequest
    {
        public readonly RegistrationApplicationId ProviderRegistrationApplicationId;
        public readonly ContactId ContactId;
        public readonly Name ContactName;
        public readonly Email ContactEmail;
        public readonly Business.RegistrationApplication.Contacts.ValueObjects.Address ContactAddress;

        public static Validation<ValidationError, AddContactRequest> Create(RequestDto req)
        {
            //validations and so on
            throw new NotImplementedException();
        }

        private AddContactRequest(
            RegistrationApplicationId providerRegistrationApplicationId, 
            ContactId contactId, 
            Name contactName, 
            Email contactEmail,
            Business.RegistrationApplication.Contacts.ValueObjects.Address contactAddress)
        {
            ProviderRegistrationApplicationId = providerRegistrationApplicationId;
            ContactId = contactId;
            ContactName = contactName;
            ContactEmail = contactEmail;
            ContactAddress = contactAddress;
        }

        public sealed class RequestDto
        {
            public readonly Guid ProviderRegistrationApplicationId;
            public readonly Guid ContactId;
            public readonly string ContactName;
            public readonly string ContactEmail;
            public readonly Guid ContactCityId;
            public readonly Guid ContactProvinceId;

            public RequestDto(
                Guid providerRegistrationApplicationId, 
                Guid contactId, 
                string contactName, 
                string contactEmail, 
                Guid contactCityId, 
                Guid contactProvinceId)
            {
                ProviderRegistrationApplicationId = providerRegistrationApplicationId;
                ContactId = contactId;
                ContactName = contactName;
                ContactEmail = contactEmail;
                ContactCityId = contactCityId;
                ContactProvinceId = contactProvinceId;
            }
        }
    }
}
