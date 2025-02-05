﻿using LanguageExt;

namespace ValueObjects
{
    public sealed class FileStorageId : ValueObject
    {
        public readonly Guid Value;

        public static Either<ValidationError, FileStorageId> Create(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
            {
                return new ValidationError(nameof(FileStorageId), ValidationErrorCode.Required);
            }

            Guid id;
            if (Guid.TryParse(value, out id))
            {
                return new ValidationError(nameof(FileStorageId), ValidationErrorCode.InvalidFormat);
            }

            return new FileStorageId(id);
        }

        public static FileStorageId UnsafeCreate(Guid value) 
        {
            return new FileStorageId(value);
        }

        private FileStorageId(Guid value)
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
