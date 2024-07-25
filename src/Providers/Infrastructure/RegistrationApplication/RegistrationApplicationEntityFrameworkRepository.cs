using LanguageExt;
using Providers.Business.RegistrationApplication.Contacts;
using Providers.Business.RegistrationApplication.Contacts.ValueObjects;
using Providers.Business.RegistrationApplication.Contracts.ValueObjects;
using Providers.Business.RegistrationApplication.ValueObjects;
using ValueObjects;

namespace Providers.Infrastructure.RegistrationApplication
{
    public sealed class RegistrationApplicationEntityFrameworkRepository : Business.RegistrationApplication.RegistrationApplicationRepository
    {
        public void Create(Business.RegistrationApplication.RegistrationApplication application)
        {
            var dbEntity = BuildDbEntity(application);
            //dbContext.Save(dbEntity);
        }

        public bool ExistByProviderName(Name providerName)
        {
            throw new NotImplementedException();
        }

        public Option<Business.RegistrationApplication.RegistrationApplication> SearchById(RegistrationApplicationId id)
        {
            //code to search the db entity by id.

            var dbEntity = new EntityFramework.Models.ProviderRegistrationApplication();
            return BuildDomainEntity(dbEntity);
        }

        private EntityFramework.Models.ProviderRegistrationApplication BuildDbEntity(
            Business.RegistrationApplication.RegistrationApplication application)
        {
            return new EntityFramework.Models.ProviderRegistrationApplication
            {
                Id = application.Id.Value,
                ProviderName = application.Provider.Name.Value,
                ProviderStreet = application.Provider.Address.Street.Value,
                ProviderPostalCode = application.Provider.Address.PostalCode.Value,
                ProviderProvinceId = application.Provider.Address.ProvinceId.Value,
                ProviderCityId = application.Provider.Address.CityId.Value,
                Contacts = application.Contacts.Map(BuildContactDbEntity).ToList(),
                ContractId = application.Contract.MatchUnsafe(Some: x => x.Id.Value, None: () => (Guid?)null),
                ContractFileStorageId = application.Contract.MatchUnsafe(Some: x => x.StorageId.Value, None: () => (Guid?)null),
                ContractAcceptanceDate = application.Contract
                    .Map(contract => (DateTime?)contract.ContractAcceptanceDate)
                    .IfNoneUnsafe(() => (DateTime?)null)
            };

            EntityFramework.Models.ProviderRegistrationApplicationContact BuildContactDbEntity(Contact contact)
            {
                return new EntityFramework.Models.ProviderRegistrationApplicationContact
                {
                    Id = contact.Id.Value,
                    Name = contact.Name.Value,
                    Email = contact.Email.Value,
                    ProvinceId = contact.Address.ProvinceId.Value,
                    CityId = contact.Address.CityId.Value
                };
            }
        }

        private Business.RegistrationApplication.RegistrationApplication BuildDomainEntity(
            EntityFramework.Models.ProviderRegistrationApplication dbEntity)
        {
            return new Business.RegistrationApplication.RegistrationApplication(
                id: RegistrationApplicationId.UnsafeCreate(dbEntity.Id),
                providerName: Name.UnsafeCreate(dbEntity.ProviderName),
                providerAddress: Business.RegistrationApplication.ValueObjects.Address.UnsafeCreate(
                    street: dbEntity.ProviderStreet,
                    postalCode: dbEntity.ProviderPostalCode,
                    provinceId: dbEntity.ProviderProvinceId,
                    cityId: dbEntity.ProviderCityId),
                contacts: dbEntity.Contacts.Map(BuildContactDomainEntity).ToList(),
                contract: BuildContractDomainEntity());

            Contact BuildContactDomainEntity(
                EntityFramework.Models.ProviderRegistrationApplicationContact contactDbEntity)
            {
                return new Contact(
                    id: ContactId.UnsafeCreate(contactDbEntity.Id),
                    name: Name.UnsafeCreate(contactDbEntity.Name),
                    email: Email.UnsafeCreate(contactDbEntity.Email),
                    address: Business.RegistrationApplication.Contacts.ValueObjects.Address.UnsafeCreate(
                        provinceId: contactDbEntity.ProvinceId,
                        cityId: contactDbEntity.CityId));
            }

            Option<Business.RegistrationApplication.Contracts.Contract> BuildContractDomainEntity()
            {
                if (!dbEntity.ContractId.HasValue || !dbEntity.ContractFileStorageId.HasValue ) return Prelude.None;

                return new Business.RegistrationApplication.Contracts.Contract(
                    id: ContractId.UnsafeCreate(dbEntity.ContractId.Value),
                    storageId: FileStorageId.UnsafeCreate(dbEntity.ContractFileStorageId.Value),
                    contractAcceptanceDate: dbEntity.ContractAcceptanceDate.ToOption());
            }
        }
    }
}
