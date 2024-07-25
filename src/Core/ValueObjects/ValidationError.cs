namespace ValueObjects
{
    public enum ValidationErrorCode
    {
        Required,
        MaximumLengthExceeded,
        InvalidFormat
    }

    public sealed class ValidationError 
    {
        public readonly string FieldId;
        public readonly ValidationErrorCode Code;

        public ValidationError(string fieldId, ValidationErrorCode code)
        {
            FieldId = fieldId;
            Code = code;
        }
    }
}
