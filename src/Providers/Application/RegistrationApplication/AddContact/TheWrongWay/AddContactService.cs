using LanguageExt;
using Providers.Business.RegistrationApplication;

namespace Providers.Application.RegistrationApplication.AddContact.TheWrongWay
{
    public sealed class AddContactService
    {
        private readonly RegistrationApplicationRepository registrationApplicationRepository;

        public AddContactService(RegistrationApplicationRepository registrationApplicationRepository)
        {
            this.registrationApplicationRepository = registrationApplicationRepository;
        }

        Either<AddContactError, Business.RegistrationApplication.RegistrationApplication> Add(AddContactRequest request)
        {
            return
                from application in SearchRegistrationApplication(request.ProviderRegistrationApplicationId)
                from _1 in CheckIfContactAlreadyExist(request, application)
                from contact in BuildContact(request)
                from _2 in AddContact(application, contact)
                select application;

        }
        private Either<AddContactError, Business.RegistrationApplication.RegistrationApplication> SearchRegistrationApplication(
            Business.RegistrationApplication.ValueObjects.RegistrationApplicationId id)
        {
            return registrationApplicationRepository
                .SearchById(id)
                .ToEither(() => AddContactError.ProviderRegistrationApplicationNotFound);
        }

        private Either<AddContactError, Unit> CheckIfContactAlreadyExist(
            AddContactRequest request,
            Business.RegistrationApplication.RegistrationApplication application)
        {
            if (application.Contacts.Any(contact => contact.Email == request.ContactEmail))
            {
                return AddContactError.DuplicatedContact;
            }
            return Prelude.unit;
        }

        private Either<AddContactError, Business.RegistrationApplication.Contacts.Contact> BuildContact(AddContactRequest request)
        {
            return new Business.RegistrationApplication.Contacts.Contact(
                id: request.ContactId,
                name: request.ContactName,
                email: request.ContactEmail,
                address: request.ContactAddress);
        }

        private Either<AddContactError, Unit> AddContact(
            Business.RegistrationApplication.RegistrationApplication application,
            Business.RegistrationApplication.Contacts.Contact contact)
        {
            application.Contacts.Add(contact);
            return Prelude.unit;
        }
    }
}
