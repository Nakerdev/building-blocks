using LanguageExt;
using LanguageExt.UnsafeValueAccess;
using Providers.Business.RegistrationApplication.ValueObjects;
using ValueObjects;

namespace Providers.Application.RegistrationApplication.Create
{
    public sealed class ProviderRegistrationApplicationRequest
    {
        public readonly RegistrationApplicationId Id;
        public readonly Name ProviderName;
        public readonly Address ProviderAddress;

        public static Validation<ValidationError, ProviderRegistrationApplicationRequest> Create(RequestDto req) 
        { 
            var idCreationResult = RegistrationApplicationId.Create(req.RegistrationApplicationId);
            var nameCreationResult = Name.Create(req.ProviderName);
            var addressCreationResult = Address.Create(
                street: req.ProviderStreet, 
                postalCode: req.ProviderPostalCode, 
                provinceId: req.ProviderProvinceId, 
                cityId: req.ProviderCityId);

            if(idCreationResult.IsLeft 
                || nameCreationResult.IsLeft 
                || addressCreationResult.IsFail) 
            {
                var validationErrors = Prelude.Seq<ValidationError>();
                idCreationResult.IfLeft(error => validationErrors = validationErrors.Add(error));
                nameCreationResult.IfLeft(error => validationErrors = validationErrors.Add(error));
                addressCreationResult.IfFail(errors => validationErrors = validationErrors.Append(errors));
                return validationErrors;
            }

            return new ProviderRegistrationApplicationRequest(
                id: idCreationResult.ValueUnsafe(),
                providerName: nameCreationResult.ValueUnsafe(),
                providerAddress: addressCreationResult.ToOption().ValueUnsafe());
        }

        private ProviderRegistrationApplicationRequest(
            RegistrationApplicationId id, 
            Name providerName, 
            Address providerAddress)
        {
            Id = id;
            ProviderName = providerName;
            ProviderAddress = providerAddress;
        }

        public sealed class RequestDto 
        {
            public readonly string RegistrationApplicationId;
            public readonly string ProviderName;
            public readonly string ProviderStreet;
            public readonly string ProviderPostalCode;
            public readonly string ProviderProvinceId;
            public readonly string ProviderCityId;

            public RequestDto(
                string registrationApplicationId, 
                string providerName, 
                string providerStreet, 
                string providerPostalCode, 
                string providerProvinceId, 
                string providerCityId)
            {
                RegistrationApplicationId = registrationApplicationId;
                ProviderName = providerName;
                ProviderStreet = providerStreet;
                ProviderPostalCode = providerPostalCode;
                ProviderProvinceId = providerProvinceId;
                ProviderCityId = providerCityId;
            }
        }
    }
}
