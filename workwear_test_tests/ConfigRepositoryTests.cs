using System;
using workwear_test;
using workwear_test.Repositories;
using Xunit;

namespace workwear_test_tests
{
    public class ConfigRepositoryTests
    {
        [Fact]
        public void CanAddKeyValue()
        {
            // arrange
            var repo = new ConfigRepository();

            // act
            repo.Add("language", "en-au");

            // assert
            Assert.Equal("en-au", repo.Get("language"));
        }

        [Fact]
        public void RejectsLargeValueOnAdd()
        {
            // arrange
            var repo = new ConfigRepository();

            // act
            // assert
            var value = new string('a', Constants.Validation.MaxValueLength + 1);
            var ex = Assert.Throws<ArgumentException>(() => repo.Add("somekey", value));
            Assert.StartsWith(Constants.Validation.ValueLengthErrorMessage, ex.Message);

            value = new string('a', Constants.Validation.MaxValueLength);
            repo.Add("somekey", value);
            Assert.Equal(value, repo.Get("somekey"));
        }

        [Fact]
        public void RejectsLargeValueOnUpdate()
        {
            // arrange
            var repo = new ConfigRepository();

            // act
            // assert
            repo.Add("somekey", "somevalue");

            var value = new string('a', Constants.Validation.MaxValueLength + 1);
            var ex = Assert.Throws<ArgumentException>(() => repo.Update("somekey", value));
            Assert.StartsWith(Constants.Validation.ValueLengthErrorMessage, ex.Message);

            value = new string('a', Constants.Validation.MaxValueLength);
            repo.Update("somekey", value);
            Assert.Equal(value, repo.Get("somekey"));
        }

        [Fact]
        public void CanUpdateKeyValue()
        {
            // arrange
            var repo = new ConfigRepository();

            // act
            repo.Add("language", "en-au");
            repo.Update("language", "en-us");

            // assert
            Assert.Equal("en-us", repo.Get("language"));
        }

        [Fact]
        public void KeysAreCaseInsensitive()
        {
            // arrange
            var repo = new ConfigRepository();

            // act 
            repo.Add("LANGUAGE", "en-au");

            // assert
            Assert.Equal("en-au", repo.Get("language"));
        }

        [Theory]
        // key alpha-numberic
        [InlineData("country123")]
        // key with hyphen
        [InlineData("country-name")]
        // key with period
        [InlineData("country.name")]
        // key with tilde
        [InlineData("country~name")]
        public void AcceptsValidKeys(string key)
        {
            // arrange
            var repo = new ConfigRepository();

            // act
            repo.Add(key, "somevalue");

            // assert
            Assert.Equal("somevalue", repo.Get(key));
        }

        [Theory]
        // non alpha-numberic characters
        [InlineData("country#")]
        // with space
        [InlineData("country name")]
        // longer than 32 characters
        [InlineData("loooooooooooooooooooooooooooooongkey")]
        public void RejectsInvalidKeys(string key)
        {
            // arrange
            var repo = new ConfigRepository();

            // act
            // assert
            var ex = Assert.Throws<ArgumentException>(() => repo.Add(key, "somevalue"));
            Assert.StartsWith(Constants.Validation.KeyValidationErrorMessage, ex.Message);
        }

    }
}
