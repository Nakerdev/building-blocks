using LanguageExt;
using Providers.Business.RegistrationApplication;
using Providers.Business.RegistrationApplication.Contacts;

namespace Providers.Application.RegistrationApplication.Create
{
    public sealed class ProviderRegistrationApplicationCreator
    {
        private readonly RegistrationApplicationRepository registrationApplicationRepository;

        public ProviderRegistrationApplicationCreator(RegistrationApplicationRepository registrationApplicationRepository)
        {
            this.registrationApplicationRepository = registrationApplicationRepository;
        }

        Either<ProviderRegistrationApplicationCreationError, Business.RegistrationApplication.RegistrationApplication> Create(
            ProviderRegistrationApplicationRequest request)
        {
            if(registrationApplicationRepository.ExistByProviderName(request.ProviderName)) 
            {
                return ProviderRegistrationApplicationCreationError.RegistrationApplicationAlreadyExist;
            }

            var application = new Business.RegistrationApplication.RegistrationApplication(
                id: request.Id,
                providerName: request.ProviderName,
                providerAddress: request.ProviderAddress,
                contacts: new List<Contact>(),
                contract: Prelude.None);
            registrationApplicationRepository.Create(application);
            return application;
        }
    }

    public enum ProviderRegistrationApplicationCreationError
    {
        RegistrationApplicationAlreadyExist
    }
}
