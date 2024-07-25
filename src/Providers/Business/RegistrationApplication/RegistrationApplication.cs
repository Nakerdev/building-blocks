using Providers.Business.RegistrationApplication.Contacts;
using Providers.Business.RegistrationApplication.ValueObjects;
using ValueObjects;

namespace Providers.Business.RegistrationApplication
{
    public sealed class RegistrationApplication
    {
        public readonly RegistrationApplicationId Id;
        public readonly ProviderDto Provider;
        public readonly List<Contact> Contacts;
        //Contracts

        public RegistrationApplication(
            RegistrationApplicationId id, 
            ProviderDto provider, 
            List<Contact> contacts)
        {
            Id = id;
            Provider = provider;
            Contacts = contacts;
        }

        public sealed class ProviderDto  
        { 
            public readonly Name ProviderName;
            public readonly Address ProviderAddress;

            public ProviderDto(Name providerName, Address providerAddress)
            {
                ProviderName = providerName;
                ProviderAddress = providerAddress;
            }
        }
    }
}
