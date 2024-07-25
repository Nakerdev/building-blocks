using LanguageExt;
using Providers.Business.RegistrationApplication.Contracts.ValueObjects;
using ValueObjects;

namespace Providers.Business.RegistrationApplication.Contracts
{
    public sealed class Contract
    {
        public readonly ContractId Id;
        public readonly FileStorageId StorageId;
        public readonly Option<DateTime> ContractAcceptanceDate;

        public Contract(
            ContractId id, 
            FileStorageId storageId, 
            Option<DateTime> contractAcceptanceDate)
        {
            Id = id;
            StorageId = storageId;
            ContractAcceptanceDate = contractAcceptanceDate;
        }
    }
}
