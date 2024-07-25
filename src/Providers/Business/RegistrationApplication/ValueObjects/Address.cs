using LanguageExt;
using LanguageExt.UnsafeValueAccess;
using ValueObjects;

namespace Providers.Business.RegistrationApplication.ValueObjects
{
    public sealed class Address : ValueObject
    {
        public readonly Street Street;
        public readonly PostalCode PostalCode;
        public readonly CityId CityId;
        public readonly ProvinceId ProvinceId;

        public static Validation<ValidationError, Address> Create(
            string street,
            string postalCode, 
            string cityId,
            string provinceId)
        {
            var streetResult = Street.Create(street, "Address.Street");
            var postalCodeResult = PostalCode.Create(postalCode, "Address.PostalCode");
            var cityIdResult = CityId.Create(cityId, "Address.CityId");
            var provinceIdResult = ProvinceId.Create(provinceId, "Address.ProvinceId");

            if(streetResult.IsLeft 
                || postalCodeResult.IsLeft 
                || cityIdResult.IsLeft 
                || provinceIdResult.IsLeft)
            {
                var validationErrors = new Seq<ValidationError>();
                streetResult.IfLeft(error => validationErrors = validationErrors.Add(error));
                postalCodeResult.IfLeft(error => validationErrors = validationErrors.Add(error));
                cityIdResult.IfLeft(error => validationErrors = validationErrors.Add(error));
                provinceIdResult.IfLeft(error => validationErrors = validationErrors.Add(error));
                return validationErrors;
            }

            return new Address(
                street: streetResult.ValueUnsafe(), 
                postalCode: postalCodeResult.ValueUnsafe(), 
                cityId: cityIdResult.ValueUnsafe(), 
                provinceId: provinceIdResult.ValueUnsafe());
        }

        public static Address UnsafeCreate(
            string street, 
            int postalCode, 
            Guid provinceId, 
            Guid cityId) 
        { 
            return new Address(
                street: Street.UnsafeCreate(street),
                postalCode: PostalCode.UnsafeCreate(postalCode),
                provinceId: ProvinceId.UnsafeCreate(provinceId),
                cityId: CityId.UnsafeCreate(cityId));
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
