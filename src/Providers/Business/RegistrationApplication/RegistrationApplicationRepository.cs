using LanguageExt;
using Providers.Business.RegistrationApplication.ValueObjects;
using ValueObjects;

namespace Providers.Business.RegistrationApplication
{
    public interface RegistrationApplicationRepository
    {
        void Create(RegistrationApplication application);
        Option<RegistrationApplication> SearchById(RegistrationApplicationId id);
        bool ExistByProviderName(Name providerName);
    }
}
