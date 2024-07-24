using LanguageExt;
using System.Xml;
using ValueObjects;

namespace Providers.Business.RegistrationApplication.ValueObjects
{
    public sealed class Address : ValueObject
    {
        private readonly Street Street;
        private readonly PostalCode PostalCode;
        private readonly CityId CityId;
        private readonly ProvinceId ProvinceId;

        public static Validation<ValidationError, Address> Create(
            string street,
            string postalCode, 
            string cityId,
            string provinceId)
        {
            var streetResult = Street.Create(street);
            var postalCodeResult = PostalCode.Create(postalCode);
            var cityIdResult = CityId.Create(postalCode);
            var provinceIdResult = ProvinceId.Create(postalCode);

            
        }

        public Address(
            Street street, 
            PostalCode postalCode, 
            CityId cityId, 
            ProvinceId provinceId)
        {
            Street = street;
            PostalCode = postalCode;
            CityId = cityId;
            ProvinceId = provinceId;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street.GetEqualityComponents();
            yield return PostalCode.GetEqualityComponents();
            yield return CityId.GetEqualityComponents();
            yield return ProvinceId.GetEqualityComponents();
        }

        public override string ToString()
        {
            return $"{Street.ToString()}, {PostalCode.ToString}, {ProvinceId.ToString()} - {CityId.ToString}";
        }
    }
}
