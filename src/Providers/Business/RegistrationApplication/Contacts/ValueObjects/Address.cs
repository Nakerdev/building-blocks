using LanguageExt;
using LanguageExt.UnsafeValueAccess;
using Providers.Business.RegistrationApplication.ValueObjects;
using ValueObjects;

namespace Providers.Business.RegistrationApplication.Contacts.ValueObjects
{
    public sealed class Address : ValueObject
    {
        private readonly CityId CityId;
        private readonly ProvinceId ProvinceId;

        public static Validation<ValidationError, Address> Create(
            string cityId,
            string provinceId)
        {
            var cityIdResult = CityId.Create(cityId, "Address.CityId");
            var provinceIdResult = ProvinceId.Create(provinceId, "Address.ProvinceId");

            if (cityIdResult.IsLeft || provinceIdResult.IsLeft)
            {
                var validationErrors = new Seq<ValidationError>();
                cityIdResult.IfLeft(error => validationErrors = validationErrors.Add(error));
                provinceIdResult.IfLeft(error => validationErrors = validationErrors.Add(error));
                return validationErrors;
            }

            return new Address(
                cityId: cityIdResult.ValueUnsafe(),
                provinceId: provinceIdResult.ValueUnsafe());
        }

        public Address(
            CityId cityId,
            ProvinceId provinceId)
        {
            CityId = cityId;
            ProvinceId = provinceId;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Street.GetEqualityComponents();
            yield return PostalCode.GetEqualityComponents();
        }

        public override string ToString()
        {
            return $"{ProvinceId.ToString()} - {CityId.ToString}";
        }
    }
}
