using Developer.Notifications.Patterns;
using System.Text.RegularExpressions;

namespace Developer.Notifications.TestProject
{
    public class DevNotifRegexPatternsTests
    {
        [Fact]
        public void WhenEmailRegexPatternReceivesValidEmailThenRegexMatches()
        {
            var result = Regex.IsMatch("john.doe@example.com", DevNotifRegexPatterns.EmailRegexPattern);

            Assert.True(result);
        }

        [Fact]
        public void WhenEmailRegexPatternReceivesInvalidEmailThenRegexDoesNotMatch()
        {
            var result = Regex.IsMatch("john..doe@", DevNotifRegexPatterns.EmailRegexPattern);

            Assert.False(result);
        }

        [Theory]
        [InlineData("https://www.example.com/path")]
        [InlineData("http://localhost:5000/api")]
        public void WhenUrlRegexPatternReceivesSupportedUrlsThenRegexMatches(string url)
        {
            var result = Regex.IsMatch(url, DevNotifRegexPatterns.UrlRegexPattern);

            Assert.True(result);
        }

        [Fact]
        public void WhenUrlRegexPatternReceivesUnsupportedSchemeThenRegexDoesNotMatch()
        {
            var result = Regex.IsMatch("ftp://example.com", DevNotifRegexPatterns.UrlRegexPattern);

            Assert.False(result);
        }

        [Fact]
        public void WhenOnlyNumbersPatternIsUsedInReplaceThenNonDigitsAreRemoved()
        {
            var result = Regex.Replace("12a-3", DevNotifRegexPatterns.OnlyNumbersPattern, string.Empty);

            Assert.Equal("123", result);
        }

        [Theory]
        [InlineData("a!", true)]
        [InlineData("!!", false)]
        public void WhenOnlyLettersAndNumbersPatternIsUsedThenRegexReflectsCurrentBehavior(string value, bool expected)
        {
            var result = Regex.IsMatch(value, DevNotifRegexPatterns.OnlyLettersAndNumbersPattern);

            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData("AB1234", true)]
        [InlineData("0000", false)]
        [InlineData("AB", false)]
        public void WhenPassportRegexPatternReceivesExpectedFormatsThenRegexReturnsExpectedResult(string value, bool expected)
        {
            var result = Regex.IsMatch(value, DevNotifRegexPatterns.PassportRegexPattern);

            Assert.Equal(expected, result);
        }
    }
}