using Developer.Notifications.TestProject.Helpers;
using System.Text.RegularExpressions;

namespace Developer.Notifications.TestProject
{
    public class BoolValidationContractTests : ContractValidationTestBase
    {
        [Fact]
        public void WhenIsFalseRuleIsEvaluatedThenCurrentBehaviorIsApplied()
        {
            AssertNotification(sut => sut.IsFalse(false, "Message"));
            AssertNotification(sut => sut.IsFalse(false, "flag", "Message"), "flag");
            AssertNoNotifications(sut => sut.IsFalse(true, "Message"));
        }

        [Fact]
        public void WhenIsTrueRuleIsEvaluatedThenCurrentBehaviorIsApplied()
        {
            AssertNotification(sut => sut.IsTrue(true, "Message"));
            AssertNotification(sut => sut.IsTrue(true, "flag", "Message"), "flag");
            AssertNoNotifications(sut => sut.IsTrue(false, "Message"));
        }

        [Fact]
        public void WhenBoolNullabilityRulesAreEvaluatedThenCurrentBehaviorIsApplied()
        {
            AssertNotification(sut => sut.IsNull((bool?)null, "Message"));
            AssertNotification(sut => sut.IsNull((bool?)null, "flag", "Message"), "flag");
            AssertNoNotifications(sut => sut.IsNull((bool?)true, "Message"));

            AssertNotification(sut => sut.IsNotNull((bool?)true, "Message"));
            AssertNotification(sut => sut.IsNotNull((bool?)false, "flag", "Message"), "flag");
            AssertNoNotifications(sut => sut.IsNotNull((bool?)null, "Message"));
        }
    }

    public class RegexValidationContractTests : ContractValidationTestBase
    {
        [Fact]
        public void WhenMatchesRuleIsEvaluatedThenItAddsNotificationOnlyForMatches()
        {
            AssertNotification(sut => sut.Matches("abc", "^abc$", "Message"));
            AssertNotification(sut => sut.Matches("abc", "^abc$", "field", "Message"), "field");
            AssertNoNotifications(sut => sut.Matches("ab", "^abc$", "Message"));
        }

        [Fact]
        public void WhenNotMatchesRuleIsEvaluatedThenItAddsNotificationOnlyForNonMatches()
        {
            AssertNotification(sut => sut.NotMatches("ab", "^abc$", "Message"));
            AssertNotification(sut => sut.NotMatches("ab", "^abc$", "field", "Message"), "field");
            AssertNoNotifications(sut => sut.NotMatches("abc", "^abc$", "Message"));
        }

        [Fact]
        public void WhenRegexMethodsReceiveNullThenNullIsHandledAsEmptyString()
        {
            AssertNotification(sut => sut.Matches(null, "^$", "Message"));
            AssertNoNotifications(sut => sut.NotMatches(null, "^$", "Message"));
        }
    }

    public class EmailValidationContractTests : ContractValidationTestBase
    {
        [Fact]
        public void WhenIsEmailRuleIsEvaluatedThenItUsesMatchesBehavior()
        {
            AssertNotification(sut => sut.IsEmail("user@example.com", "Message"));
            AssertNotification(sut => sut.IsEmail("user@example.com", "email", "Message"), "email");
            AssertNoNotifications(sut => sut.IsEmail("invalid", "Message"));
        }

        [Fact]
        public void WhenIsEmailOrEmptyRuleIsEvaluatedThenEmptyValuesAreIgnored()
        {
            AssertNoNotifications(sut => sut.IsEmailOrEmpty(string.Empty, "Message"));
            AssertNoNotifications(sut => sut.IsEmailOrEmpty(null, "Message"));
            AssertNotification(sut => sut.IsEmailOrEmpty("user@example.com", "Message"));
            AssertNotification(sut => sut.IsEmailOrEmpty("user@example.com", "email", "Message"), "email");
        }

        [Fact]
        public void WhenIsNotEmailRuleIsEvaluatedThenItUsesNotMatchesBehavior()
        {
            AssertNotification(sut => sut.IsNotEmail("invalid", "Message"));
            AssertNotification(sut => sut.IsNotEmail("invalid", "email", "Message"), "email");
            AssertNoNotifications(sut => sut.IsNotEmail("user@example.com", "Message"));
        }
    }

    public class CreditCardValidationTests : ContractValidationTestBase
    {
        [Fact]
        public void WhenCreditCardNumberIsValidThenNoNotificationIsAdded()
        {
            AssertNoNotifications(sut => sut.IsCreditCard("4111111111111111", "Message"));
            AssertNoNotifications(sut => sut.IsCreditCard("4111 1111-1111 1111", "Message"));
        }

        [Fact]
        public void WhenCreditCardNumberIsInvalidThenNotificationIsAdded()
        {
            AssertNotification(sut => sut.IsCreditCard("4111111111111112", "Message"));
            AssertNotification(sut => sut.IsCreditCard("4111111111111112", "card", "Message"), "card");
        }

        [Fact]
        public void WhenCreditCardNumberIsBlankThenNotificationIsAdded()
        {
            AssertNotification(sut => sut.IsCreditCard(string.Empty, "Message"));
            AssertNotification(sut => sut.IsCreditCard(string.Empty, "card", "Message"), "card");
        }

        [Fact]
        public void WhenCreditCardNumberIsNullThenCurrentImplementationThrows()
        {
            Assert.Throws<ArgumentNullException>(() => Execute(sut => sut.IsCreditCard(null, "Message")));
            Assert.Throws<ArgumentNullException>(() => Execute(sut => sut.IsCreditCard(null, "card", "Message")));
        }
    }

    public class DocumentValidationContractTests : ContractValidationTestBase
    {
        [Fact]
        public void WhenPassportRuleIsEvaluatedThenItUsesRegexMatchBehavior()
        {
            AssertNotification(sut => sut.IsPassport("AB1234", "Message"));
            AssertNotification(sut => sut.IsPassport("AB1234", "passport", "Message"), "passport");
            AssertNoNotifications(sut => sut.IsPassport("00", "Message"));
        }

        [Fact]
        public void WhenRgLengthIsInvalidThenNoNotificationIsAdded()
        {
            AssertNoNotifications(sut => sut.IsRG("123", "Message"));
            AssertNoNotifications(sut => sut.IsRG("123", "document", "Message"));
        }

        [Fact]
        public void WhenRgCheckDigitIsInvalidThenNotificationIsAdded()
        {
            AssertNotification(sut => sut.IsRG("000000001", "Message"));
            AssertNotification(sut => sut.IsRG("000000001", "document", "Message"), "document");
        }

        [Fact]
        public void WhenRgIsAllZerosThenCurrentAlgorithmAcceptsIt()
        {
            AssertNoNotifications(sut => sut.IsRG("000000000", "Message"));
        }

        [Fact]
        public void WhenCpfRuleIsEvaluatedThenOnlyInvalidValuesAddNotifications()
        {
            AssertNoNotifications(sut => sut.IsCPF("52998224725", "Message"));
            AssertNotification(sut => sut.IsCPF("52998224724", "Message"));
            AssertNotification(sut => sut.IsCPF("52998224724", "cpf", "Message"), "cpf");
        }

        [Fact]
        public void WhenCpfIsNullThenCurrentImplementationThrows()
        {
            Assert.Throws<ArgumentNullException>(() => Execute(sut => sut.IsCPF(null, "Message")));
            Assert.Throws<ArgumentNullException>(() => Execute(sut => sut.IsCPF(null, "cpf", "Message")));
        }

        [Fact]
        public void WhenCnpjRuleIsEvaluatedThenOnlyInvalidValuesAddNotifications()
        {
            AssertNoNotifications(sut => sut.IsCNPJ("04.252.011/0001-10", "Message"));
            AssertNotification(sut => sut.IsCNPJ("04.252.011/0001-11", "Message"));
            AssertNotification(sut => sut.IsCNPJ("04.252.011/0001-11", "cnpj", "Message"), "cnpj");
            AssertNotification(sut => sut.IsCNPJ("   ", "Message"));
        }
    }

    public class GuidValidationContractTests : ContractValidationTestBase
    {
        private static readonly Guid FirstGuid = Guid.Parse("11111111-1111-1111-1111-111111111111");
        private static readonly Guid SecondGuid = Guid.Parse("22222222-2222-2222-2222-222222222222");

        [Fact]
        public void WhenGuidNullabilityRulesAreEvaluatedThenCurrentBehaviorIsApplied()
        {
            AssertNotification(sut => sut.IsNull((Guid?)null, "Message"));
            AssertNotification(sut => sut.IsNull((Guid?)null, "id", "Message"), "id");
            AssertNoNotifications(sut => sut.IsNull((Guid?)FirstGuid, "Message"));

            AssertNotification(sut => sut.IsNotNull((Guid?)FirstGuid, "Message"));
            AssertNotification(sut => sut.IsNotNull((Guid?)FirstGuid, "id", "Message"), "id");
            AssertNoNotifications(sut => sut.IsNotNull((Guid?)null, "Message"));
        }

        [Fact]
        public void WhenGuidEqualityRulesAreEvaluatedThenCurrentBehaviorIsApplied()
        {
            AssertNotification(sut => sut.AreEquals(FirstGuid, FirstGuid, "Message"));
            AssertNotification(sut => sut.AreEquals(FirstGuid, FirstGuid, "id", "Message"), "id");
            AssertNoNotifications(sut => sut.AreEquals(FirstGuid, SecondGuid, "Message"));

            AssertNotification(sut => sut.AreNotEquals(FirstGuid, SecondGuid, "Message"));
            AssertNotification(sut => sut.AreNotEquals(FirstGuid, SecondGuid, "id", "Message"), "id");
            AssertNoNotifications(sut => sut.AreNotEquals(FirstGuid, FirstGuid, "Message"));
        }

        [Fact]
        public void WhenGuidContainmentRulesAreEvaluatedThenCurrentBehaviorIsApplied()
        {
            var list = new[] { FirstGuid, SecondGuid };

            AssertNotification(sut => sut.Contains(FirstGuid, list, "Message"));
            AssertNotification(sut => sut.Contains(FirstGuid, list, "id", "Message"), "id");
            AssertNoNotifications(sut => sut.Contains(Guid.Empty, list, "Message"));

            AssertNotification(sut => sut.NotContains(Guid.Empty, list, "Message"));
            AssertNotification(sut => sut.NotContains(Guid.Empty, list, "id", "Message"), "id");
            AssertNoNotifications(sut => sut.NotContains(FirstGuid, list, "Message"));
        }
    }

    public class ObjectValidationContractTests : ContractValidationTestBase
    {
        [Fact]
        public void WhenObjectNullabilityRulesAreEvaluatedThenCurrentBehaviorIsApplied()
        {
            var value = new object();

            AssertNotification(sut => sut.IsNull((object)null, "Message"));
            AssertNotification(sut => sut.IsNull((object)null, "object", "Message"), "object");
            AssertNoNotifications(sut => sut.IsNull(value, "Message"));

            AssertNotification(sut => sut.IsNotNull(value, "Message"));
            AssertNotification(sut => sut.IsNotNull(value, "object", "Message"), "object");
            AssertNoNotifications(sut => sut.IsNotNull((object)null, "Message"));
        }

        [Fact]
        public void WhenObjectEqualityRulesAreEvaluatedThenCurrentBehaviorIsApplied()
        {
            AssertNotification(sut => sut.AreEquals("abc", "abc", "Message"));
            AssertNotification(sut => sut.AreEquals("abc", "abc", "object", "Message"), "object");
            AssertNoNotifications(sut => sut.AreEquals("abc", "def", "Message"));

            AssertNotification(sut => sut.AreNotEquals("abc", "def", "Message"));
            AssertNotification(sut => sut.AreNotEquals("abc", "def", "object", "Message"), "object");
            AssertNoNotifications(sut => sut.AreNotEquals("abc", "abc", "Message"));
        }

        [Fact]
        public void WhenAnyObjectOperandIsNullThenEqualityRulesDoNothing()
        {
            AssertNoNotifications(sut => sut.AreEquals((object)null, "abc", "Message"));
            AssertNoNotifications(sut => sut.AreEquals("abc", (object)null, "Message"));
            AssertNoNotifications(sut => sut.AreNotEquals((object)null, "abc", "Message"));
            AssertNoNotifications(sut => sut.AreNotEquals("abc", (object)null, "Message"));
        }
    }

    public class ListValidationContractTests : ContractValidationTestBase
    {
        [Fact]
        public void WhenListNullAndEmptyRulesAreEvaluatedThenCurrentBehaviorIsApplied()
        {
            AssertNotification(sut => sut.IsNull<int>(null, "Message"));
            AssertNotification(sut => sut.IsNull<int>(null, "items", "Message"), "items");
            AssertNoNotifications(sut => sut.IsNull(new[] { 1 }, "Message"));

            AssertNotification(sut => sut.IsNotNull(new[] { 1 }, "Message"));
            AssertNotification(sut => sut.IsNotNull(new[] { 1 }, "items", "Message"), "items");
            AssertNoNotifications(sut => sut.IsNotNull<int>(null, "Message"));

            AssertNotification(sut => sut.IsEmpty(Array.Empty<int>(), "Message"));
            AssertNotification(sut => sut.IsEmpty(Array.Empty<int>(), "items", "Message"), "items");
            AssertNoNotifications(sut => sut.IsEmpty(new[] { 1 }, "Message"));

            AssertNotification(sut => sut.IsNotEmpty(new[] { 1 }, "Message"));
            AssertNotification(sut => sut.IsNotEmpty(new[] { 1 }, "items", "Message"), "items");
            AssertNoNotifications(sut => sut.IsNotEmpty(Array.Empty<int>(), "Message"));
        }

        [Fact]
        public void WhenListCountComparisonRulesAreEvaluatedThenCurrentBehaviorIsApplied()
        {
            var list = new[] { 1, 2, 3 };
            var smallList = new[] { 1 };

            AssertNotification(sut => sut.IsGreaterThan(list, 2, "Message"));
            AssertNotification(sut => sut.IsGreaterThan(list, 2L, "items", "Message"), "items");
            AssertNoNotifications(sut => sut.IsGreaterThan(smallList, 2, "Message"));

            AssertNotification(sut => sut.IsGreaterOrEqualsThan(list, 3, "Message"));
            AssertNotification(sut => sut.IsGreaterOrEqualsThan(list, 3L, "items", "Message"), "items");
            AssertNoNotifications(sut => sut.IsGreaterOrEqualsThan(smallList, 2, "Message"));

            AssertNotification(sut => sut.IsLowerThan(smallList, 2, "Message"));
            AssertNotification(sut => sut.IsLowerThan(smallList, 2L, "items", "Message"), "items");
            AssertNoNotifications(sut => sut.IsLowerThan(list, 2, "Message"));

            AssertNotification(sut => sut.IsLowerOrEqualsThan(smallList, 1, "Message"));
            AssertNotification(sut => sut.IsLowerOrEqualsThan(smallList, 1L, "items", "Message"), "items");
            AssertNoNotifications(sut => sut.IsLowerOrEqualsThan(list, 2, "Message"));
        }

        [Fact]
        public void WhenListCountComparisonReceivesNullThenNoNotificationIsAdded()
        {
            AssertNoNotifications(sut => sut.IsGreaterThan<int>(null, 2, "Message"));
            AssertNoNotifications(sut => sut.IsGreaterOrEqualsThan<int>(null, 2, "Message"));
            AssertNoNotifications(sut => sut.IsLowerThan<int>(null, 2, "Message"));
            AssertNoNotifications(sut => sut.IsLowerOrEqualsThan<int>(null, 2, "Message"));
        }
    }

    public class StringValidationContractTests : ContractValidationTestBase
    {
        private const string ValidGuidValue = "11111111-1111-1111-1111-111111111111";

        [Fact]
        public void WhenStringNullAndEmptyRulesAreEvaluatedThenCurrentBehaviorIsApplied()
        {
            AssertNotification(sut => sut.IsNull((string)null, "Message"));
            AssertNotification(sut => sut.IsNull((string)null, "text", "Message"), "text");
            AssertNoNotifications(sut => sut.IsNull("abc", "Message"));

            AssertNotification(sut => sut.IsNullOrEmpty(string.Empty, "Message"));
            AssertNotification(sut => sut.IsNullOrEmpty(string.Empty, "text", "Message"), "text");
            AssertNoNotifications(sut => sut.IsNullOrEmpty("abc", "Message"));

            AssertNotification(sut => sut.IsNullOrWhiteSpace("   ", "Message"));
            AssertNotification(sut => sut.IsNullOrWhiteSpace("   ", "text", "Message"), "text");
            AssertNoNotifications(sut => sut.IsNullOrWhiteSpace("abc", "Message"));
        }

        [Fact]
        public void WhenStringNotNullAndNotEmptyRulesAreEvaluatedThenCurrentBehaviorIsApplied()
        {
            AssertNotification(sut => sut.IsNotNull("abc", "Message"));
            AssertNotification(sut => sut.IsNotNull("abc", "text", "Message"), "text");
            AssertNoNotifications(sut => sut.IsNotNull((string)null, "Message"));

            AssertNotification(sut => sut.IsNotNullOrEmpty("abc", "Message"));
            AssertNotification(sut => sut.IsNotNullOrEmpty("abc", "text", "Message"), "text");
            AssertNoNotifications(sut => sut.IsNotNullOrEmpty(string.Empty, "Message"));

            AssertNotification(sut => sut.IsNotNullOrWhiteSpace("abc", "Message"));
            AssertNotification(sut => sut.IsNotNullOrWhiteSpace("abc", "text", "Message"), "text");
            AssertNoNotifications(sut => sut.IsNotNullOrWhiteSpace("   ", "Message"));
        }

        [Fact]
        public void WhenStringEqualityAndContainmentRulesAreEvaluatedThenCurrentBehaviorIsApplied()
        {
            AssertNotification(sut => sut.AreEquals("abc", "abc", "Message"));
            AssertNotification(sut => sut.AreEquals("abc", "abc", "text", "Message"), "text");
            AssertNoNotifications(sut => sut.AreEquals("abc", "def", "Message"));

            AssertNotification(sut => sut.AreNotEquals("abc", "def", "Message"));
            AssertNotification(sut => sut.AreNotEquals("abc", "def", "text", "Message"), "text");
            AssertNoNotifications(sut => sut.AreNotEquals("abc", "abc", "Message"));

            AssertNotification(sut => sut.Contains("abcdef", "cd", "Message"));
            AssertNotification(sut => sut.Contains("abcdef", "cd", "text", "Message"), "text");
            AssertNoNotifications(sut => sut.Contains("abcdef", "xy", "Message"));

            AssertNotification(sut => sut.NotContains("abcdef", "xy", "Message"));
            AssertNotification(sut => sut.NotContains("abcdef", "xy", "text", "Message"), "text");
            AssertNoNotifications(sut => sut.NotContains("abcdef", "cd", "Message"));
        }

        [Fact]
        public void WhenContainsReceivesNullValuesThenCurrentImplementationThrows()
        {
            Assert.Throws<NullReferenceException>(() => Execute(sut => sut.Contains(null, "a", "Message")));
            Assert.Throws<NullReferenceException>(() => Execute(sut => sut.NotContains(null, "a", "Message")));
            Assert.Throws<ArgumentNullException>(() => Execute(sut => sut.Contains("abc", null, "Message")));
            Assert.Throws<ArgumentNullException>(() => Execute(sut => sut.NotContains("abc", null, "Message")));
        }

        [Fact]
        public void WhenGuidRulesAreEvaluatedThenCurrentBehaviorIsApplied()
        {
            AssertNotification(sut => sut.IsGuid(ValidGuidValue, "Message"));
            AssertNotification(sut => sut.IsGuid(ValidGuidValue, "text", "Message"), "text");
            AssertNoNotifications(sut => sut.IsGuid("invalid", "Message"));

            AssertNotification(sut => sut.IsNotGuid("invalid", "Message"));
            AssertNotification(sut => sut.IsNotGuid("invalid", "text", "Message"), "text");
            AssertNoNotifications(sut => sut.IsNotGuid(ValidGuidValue, "Message"));
        }

        [Fact]
        public void WhenUrlRulesAreEvaluatedThenCurrentBehaviorIsApplied()
        {
            AssertNotification(sut => sut.IsUrl("https://www.example.com", "Message"));
            AssertNotification(sut => sut.IsUrl("https://www.example.com", "text", "Message"), "text");
            AssertNoNotifications(sut => sut.IsUrl("notaurl", "Message"));

            AssertNotification(sut => sut.IsNotUrl("notaurl", "Message"));
            AssertNotification(sut => sut.IsNotUrl("notaurl", "text", "Message"), "text");
            AssertNoNotifications(sut => sut.IsNotUrl("https://www.example.com", "Message"));
        }
    }
}