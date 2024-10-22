﻿using LanguageExt;
using Providers.Business.RegistrationApplication;

namespace Providers.Application.RegistrationApplication.AddContact.TheRightWay
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
                from contact in BuildContact(request)
                from newApplication in AddContact(application, contact)
                select newApplication;
        }

        private Either<AddContactError, Business.RegistrationApplication.RegistrationApplication> SearchRegistrationApplication(
            Business.RegistrationApplication.ValueObjects.RegistrationApplicationId id)
        {
            return registrationApplicationRepository
                .SearchById(id)
                .ToEither(() => AddContactError.ProviderRegistrationApplicationNotFound);
        }
        
        private Either<AddContactError, Business.RegistrationApplication.Contacts.Contact> BuildContact(AddContactRequest request)
        {
            return new Business.RegistrationApplication.Contacts.Contact(
                id: request.ContactId,
                name: request.ContactName,
                email: request.ContactEmail,
                address: request.ContactAddress);
        }

        private Either<AddContactError, Business.RegistrationApplication.RegistrationApplication> AddContact(
            Business.RegistrationApplication.RegistrationApplication application,
            Business.RegistrationApplication.Contacts.Contact contact)
        {
            return application
                .AddContact(contact)
                .MapLeft(_ => AddContactError.DuplicatedContact);
        }
    }
}
