using LanguageExt;
using Providers.Business.RegistrationApplication.Contacts;
using Providers.Business.RegistrationApplication.Contracts;
using Providers.Business.RegistrationApplication.ValueObjects;
using ValueObjects;

namespace Providers.Business.RegistrationApplication
{
    public sealed class RegistrationApplication
    {
        public readonly RegistrationApplicationId Id;
        public readonly Provider Provider;
        public readonly List<Contact> Contacts;
        public readonly Option<Contract> Contract;

        public RegistrationApplication(
            RegistrationApplicationId id,
            Provider provider,
            List<Contact> contacts,
            Option<Contract> contract)
        {
            Id = id;
            Provider = provider;
            Contacts = contacts;
            Contract = contract;
        }
    }

    public sealed class Provider
    {
        public readonly Name Name;
        public readonly Address Address;

        public Provider(Name name, Address address)
        {
            Name = name;
            Address = address;
        }
    }
}
