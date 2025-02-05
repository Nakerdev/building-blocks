﻿using LanguageExt;
using ValueObjects;

namespace Providers.Business.RegistrationApplication.ValueObjects
{
    public sealed class RegistrationApplicationId : ValueObject
    {
        public readonly Guid Value;

        public static Either<ValidationError, RegistrationApplicationId> Create(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return new ValidationError(nameof(RegistrationApplicationId), ValidationErrorCode.Required);
            }

            Guid id;
            if (Guid.TryParse(value, out id))
            {
                return new ValidationError(nameof(RegistrationApplicationId), ValidationErrorCode.InvalidFormat);
            }

            return new RegistrationApplicationId(id);
        }

        public static RegistrationApplicationId UnsafeCreate(Guid value)
        {
            return new RegistrationApplicationId(value);
        }

        private RegistrationApplicationId(Guid value)
        {
            Value = value;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}
