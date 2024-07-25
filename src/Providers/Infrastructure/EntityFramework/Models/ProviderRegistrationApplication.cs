namespace Providers.Infrastructure.EntityFramework.Models
{
    public sealed class ProviderRegistrationApplication
    {
        public Guid Id { set; get; }
        public string ProviderName { set; get; }
        public string ProviderStreet { set; get; }
        public int ProviderPostalCode { set; get; }
        public Guid ProviderProvinceId { set; get; }
        public Guid ProviderCityId { set; get; }

        public List<ProviderRegistrationApplicationContact> Contacts;
        public Guid? ContractId { set; get; }
        public Guid? ContractFileStorageId { set; get; }
        public DateTime? ContractAcceptanceDate { set; get; }
    }
}
