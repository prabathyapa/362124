using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace workwear_test.Repositories
{
    public class ConfigRepository : IConfigRepository
    {
        private readonly Dictionary<string, string> _configStore;

        public ConfigRepository()
        {
            _configStore = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
        }

        public void Add(string key, string value)
        {
            if (_configStore.ContainsKey(key))
                throw new ArgumentException(Constants.Validation.DuplicateKeyErrorMessage, nameof(key));

            var keyRegex = new Regex(Constants.Validation.KeyValidationRegex);
            if (!keyRegex.IsMatch(key))
                throw new ArgumentException(Constants.Validation.KeyValidationErrorMessage, nameof(key));

            if (value.Length > Constants.Validation.MaxValueLength)
                throw new ArgumentException(Constants.Validation.ValueLengthErrorMessage, nameof(value));

            _configStore.Add(key, value);
        }

        public void Update(string key, string value)
        {
            if (!_configStore.ContainsKey(key))
                throw new KeyNotFoundException(Constants.Validation.KeyNotFoundErrorMessage);

            if (value.Length > Constants.Validation.MaxValueLength)
                throw new ArgumentException(Constants.Validation.ValueLengthErrorMessage, nameof(value));

            _configStore[key] = value;
        }

        public string Get(string key)
        {
            if (!_configStore.ContainsKey(key))
                throw new KeyNotFoundException(Constants.Validation.KeyNotFoundErrorMessage);

            return _configStore[key];
        }
    }
}
