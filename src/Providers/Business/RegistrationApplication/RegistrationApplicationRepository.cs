using LanguageExt;
using ValueObjects;

namespace Providers.Business.RegistrationApplication
{
    public interface RegistrationApplicationRepository
    {
        void Create(RegistrationApplication application);
        bool ExistByProviderName(Name providerName);
    }
}
