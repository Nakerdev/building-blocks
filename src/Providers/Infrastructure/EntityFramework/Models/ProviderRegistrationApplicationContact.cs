namespace Providers.Infrastructure.EntityFramework.Models
{
    public class ProviderRegistrationApplicationContact
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public Guid ProvinceId { get; set; }
        public Guid CityId { get; set; }
        public ProviderRegistrationApplication ProviderRegistrationApplication { get; set; }
    }
}
