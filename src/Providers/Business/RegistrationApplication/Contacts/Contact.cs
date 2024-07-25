using Providers.Business.RegistrationApplication.Contacts.ValueObjects;
using Providers.Business.RegistrationApplication.Contracts.ValueObjects;
using ValueObjects;

namespace Providers.Business.RegistrationApplication.Contacts
{
    public sealed class Contact
    {
        public readonly ContactId Id;
        public readonly Name Name;
        public readonly Email Email;
        public readonly Address Address;

        public Contact(
            ContactId id,
            Name name,
            Email email,
            Address address)
        {
            Id = id;
            Name = name;
            Email = email;
            Address = address;
        }
    }
}
