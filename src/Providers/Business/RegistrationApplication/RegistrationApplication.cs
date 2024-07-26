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
            Name providerName,
            Address providerAddress,
            List<Contact> contacts,
            Option<Contract> contract)
        {
            Id = id;
            Provider = new Provider(name: providerName, address: providerAddress);
            Contacts = contacts;
            Contract = contract;
        }

        public Either<AddContactError, RegistrationApplication> AddContact(Contact newContact)
        {
            if (Contacts.Any(contact => contact.Email == newContact.Email)) 
            {
                return AddContactError.DuplicatedContact;
            }

            var contacts = new List<Contact>();
            contacts.AddRange(Contacts);
            contacts.Add(newContact);

            return new RegistrationApplication(
                id: Id,
                providerName: Provider.Name,
                providerAddress: Provider.Address,
                contacts: contacts,
                contract: Contract);
        }

        public enum AddContactError 
        { 
            DuplicatedContact
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
