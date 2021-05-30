namespace workwear_test
{
    public static class Constants
    {
        public struct Validation
        {
            public const string KeyValidationRegex = @"^[a-zA-Z0-9\-._~]{1,32}$";
            public const string KeyValidationErrorMessage = "Key should be alphanumeric, hyphen, period, underscore, tilde permitted, and no longer than 32 character";
            public const int MaxValueLength = 1024;
            public const string ValueLengthErrorMessage = "Values cannot be longer than 1024 characters";
            public const string DuplicateKeyErrorMessage = "Key already exists";
            public const string KeyNotFoundErrorMessage = "Key not found";
        }
    }
}
