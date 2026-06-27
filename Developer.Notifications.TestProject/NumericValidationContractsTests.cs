using Developer.Notifications.TestProject.Helpers;

namespace Developer.Notifications.TestProject
{
    public class IntValidationContractTests : ContractValidationTestBase
    {
        [Fact]
        public void WhenIntNullabilityRulesAreEvaluatedThenCurrentBehaviorIsApplied()
        {
            AssertNotification(sut => sut.IsNull((int?)null, "Message"));
            AssertNotification(sut => sut.IsNull((int?)null, "number", "Message"), "number");
            AssertNoNotifications(sut => sut.IsNull((int?)1, "Message"));

            AssertNotification(sut => sut.IsNotNull((int?)1, "Message"));
            AssertNotification(sut => sut.IsNotNull((int?)1, "number", "Message"), "number");
            AssertNoNotifications(sut => sut.IsNotNull((int?)null, "Message"));
        }

        [Fact]
        public void WhenIntComparisonRulesAreEvaluatedThenCurrentBehaviorIsApplied()
        {
            AssertNotification(sut => sut.IsGreaterThan(5, 4, "Message"));
            AssertNotification(sut => sut.IsGreaterThan(5, 4, "number", "Message"), "number");
            AssertNoNotifications(sut => sut.IsGreaterThan(4, 5, "Message"));

            AssertNotification(sut => sut.IsGreaterOrEqualsThan(5, 5, "Message"));
            AssertNotification(sut => sut.IsGreaterOrEqualsThan(5, 5, "number", "Message"), "number");
            AssertNoNotifications(sut => sut.IsGreaterOrEqualsThan(4, 5, "Message"));

            AssertNotification(sut => sut.IsLowerThan(4, 5, "Message"));
            AssertNotification(sut => sut.IsLowerThan(4, 5, "number", "Message"), "number");
            AssertNoNotifications(sut => sut.IsLowerThan(5, 4, "Message"));

            AssertNotification(sut => sut.IsLowerOrEqualsThan(5, 5, "Message"));
            AssertNotification(sut => sut.IsLowerOrEqualsThan(5, 5, "number", "Message"), "number");
            AssertNoNotifications(sut => sut.IsLowerOrEqualsThan(6, 5, "Message"));

            AssertNotification(sut => sut.AreEquals(5, 5, "Message"));
            AssertNotification(sut => sut.AreEquals(5, 5, "number", "Message"), "number");
            AssertNoNotifications(sut => sut.AreEquals(5, 6, "Message"));

            AssertNotification(sut => sut.AreNotEquals(5, 6, "Message"));
            AssertNotification(sut => sut.AreNotEquals(5, 6, "number", "Message"), "number");
            AssertNoNotifications(sut => sut.AreNotEquals(5, 5, "Message"));
        }

        [Fact]
        public void WhenIntBoundaryAndRangeRulesAreEvaluatedThenCurrentBehaviorIsApplied()
        {
            AssertNotification(sut => sut.IsMinValue(int.MinValue, "Message"));
            AssertNotification(sut => sut.IsMinValue(int.MinValue, "number", "Message"), "number");
            AssertNoNotifications(sut => sut.IsMinValue(0, "Message"));

            AssertNotification(sut => sut.IsNotMinValue(0, "Message"));
            AssertNotification(sut => sut.IsNotMinValue(0, "number", "Message"), "number");
            AssertNoNotifications(sut => sut.IsNotMinValue(int.MinValue, "Message"));

            AssertNotification(sut => sut.IsMaxValue(int.MaxValue, "Message"));
            AssertNotification(sut => sut.IsMaxValue(int.MaxValue, "number", "Message"), "number");
            AssertNoNotifications(sut => sut.IsMaxValue(0, "Message"));

            AssertNotification(sut => sut.IsNotMaxValue(0, "Message"));
            AssertNotification(sut => sut.IsNotMaxValue(0, "number", "Message"), "number");
            AssertNoNotifications(sut => sut.IsNotMaxValue(int.MaxValue, "Message"));

            AssertNotification(sut => sut.IsBetween(7, 5, 10, "Message"));
            AssertNotification(sut => sut.IsBetween(7, 5, 10, "number", "Message"), "number");
            AssertNoNotifications(sut => sut.IsBetween(4, 5, 10, "Message"));

            AssertNotification(sut => sut.IsNotBetween(4, 5, 10, "Message"));
            AssertNotification(sut => sut.IsNotBetween(4, 5, 10, "number", "Message"), "number");
            AssertNoNotifications(sut => sut.IsNotBetween(7, 5, 10, "Message"));
        }

        [Fact]
        public void WhenIntContainmentRulesAreEvaluatedThenCurrentBehaviorIsApplied()
        {
            var list = new[] { 4, 5, 6 };

            AssertNotification(sut => sut.Contains(5, list, "Message"));
            AssertNotification(sut => sut.Contains(5, list, "number", "Message"), "number");
            AssertNoNotifications(sut => sut.Contains(9, list, "Message"));

            AssertNotification(sut => sut.NotContains(9, list, "Message"));
            AssertNotification(sut => sut.NotContains(9, list, "number", "Message"), "number");
            AssertNoNotifications(sut => sut.NotContains(5, list, "Message"));
        }
    }

    public class LongValidationContractTests : ContractValidationTestBase
    {
        [Fact]
        public void WhenLongNullabilityAndComparisonRulesAreEvaluatedThenCurrentBehaviorIsApplied()
        {
            AssertNotification(sut => sut.IsNull((long?)null, "Message"));
            AssertNotification(sut => sut.IsNull((long?)null, "number", "Message"), "number");
            AssertNoNotifications(sut => sut.IsNull((long?)1, "Message"));

            AssertNotification(sut => sut.IsNotNull((long?)1, "Message"));
            AssertNotification(sut => sut.IsNotNull((long?)1, "number", "Message"), "number");
            AssertNoNotifications(sut => sut.IsNotNull((long?)null, "Message"));

            AssertNotification(sut => sut.IsGreaterThan(5L, 4L, "Message"));
            AssertNotification(sut => sut.IsGreaterThan(5L, 4L, "number", "Message"), "number");
            AssertNoNotifications(sut => sut.IsGreaterThan(4L, 5L, "Message"));

            AssertNotification(sut => sut.IsLowerThan(4L, 5L, "Message"));
            AssertNotification(sut => sut.IsLowerThan(4L, 5L, "number", "Message"), "number");
            AssertNoNotifications(sut => sut.IsLowerThan(5L, 4L, "Message"));
        }

        [Fact]
        public void WhenLongBoundaryRulesAreEvaluatedThenCurrentBehaviorIsApplied()
        {
            AssertNotification(sut => sut.IsMinValue((long)int.MinValue, "Message"));
            AssertNoNotifications(sut => sut.IsMinValue(long.MinValue, "Message"));
            AssertNotification(sut => sut.IsMinValue(long.MinValue, "number", "Message"), "number");

            AssertNotification(sut => sut.IsNotMinValue(0L, "Message"));
            AssertNotification(sut => sut.IsNotMinValue(0L, "number", "Message"), "number");
            AssertNoNotifications(sut => sut.IsNotMinValue(long.MinValue, "Message"));

            AssertNotification(sut => sut.IsMaxValue(long.MaxValue, "Message"));
            AssertNotification(sut => sut.IsMaxValue(long.MaxValue, "number", "Message"), "number");
            AssertNoNotifications(sut => sut.IsMaxValue(0L, "Message"));

            AssertNotification(sut => sut.IsNotMaxValue(0L, "Message"));
            AssertNotification(sut => sut.IsNotMaxValue(0L, "number", "Message"), "number");
            AssertNoNotifications(sut => sut.IsNotMaxValue(long.MaxValue, "Message"));
        }

        [Fact]
        public void WhenLongEqualityRangeAndContainmentRulesAreEvaluatedThenCurrentBehaviorIsApplied()
        {
            var list = new[] { 4L, 5L, 6L };

            AssertNotification(sut => sut.AreEquals(5L, 5L, "Message"));
            AssertNotification(sut => sut.AreEquals(5L, 5L, "number", "Message"), "number");
            AssertNoNotifications(sut => sut.AreEquals(5L, 6L, "Message"));

            AssertNotification(sut => sut.AreNotEquals(5L, 6L, "Message"));
            AssertNotification(sut => sut.AreNotEquals(5L, 6L, "number", "Message"), "number");
            AssertNoNotifications(sut => sut.AreNotEquals(5L, 5L, "Message"));

            AssertNotification(sut => sut.IsBetween(7L, 5L, 10L, "Message"));
            AssertNotification(sut => sut.IsBetween(7L, 5L, 10L, "number", "Message"), "number");
            AssertNoNotifications(sut => sut.IsBetween(4L, 5L, 10L, "Message"));

            AssertNotification(sut => sut.IsNotBetween(4L, 5L, 10L, "Message"));
            AssertNotification(sut => sut.IsNotBetween(4L, 5L, 10L, "number", "Message"), "number");
            AssertNoNotifications(sut => sut.IsNotBetween(7L, 5L, 10L, "Message"));

            AssertNotification(sut => sut.Contains(5L, list, "Message"));
            AssertNotification(sut => sut.Contains(5L, list, "number", "Message"), "number");
            AssertNoNotifications(sut => sut.Contains(9L, list, "Message"));

            AssertNotification(sut => sut.NotContains(9L, list, "Message"));
            AssertNotification(sut => sut.NotContains(9L, list, "number", "Message"), "number");
            AssertNoNotifications(sut => sut.NotContains(5L, list, "Message"));
        }
    }

    public class DecimalValidationContractTests : ContractValidationTestBase
    {
        [Fact]
        public void WhenDecimalRulesAreEvaluatedThenCurrentBehaviorIsApplied()
        {
            var list = new[] { 4.5m, 5.5m, 6.5m };

            AssertNotification(sut => sut.IsNull((decimal?)null, "Message"));
            AssertNotification(sut => sut.IsNull((decimal?)null, "number", "Message"), "number");
            AssertNoNotifications(sut => sut.IsNull((decimal?)1m, "Message"));

            AssertNotification(sut => sut.IsGreaterThan(5.5m, 4.5m, "Message"));
            AssertNotification(sut => sut.IsGreaterOrEqualsThan(5.5m, 5.5m, "number", "Message"), "number");
            AssertNotification(sut => sut.IsLowerThan(4.5m, 5.5m, "Message"));
            AssertNotification(sut => sut.IsLowerOrEqualsThan(5.5m, 5.5m, "number", "Message"), "number");

            AssertNotification(sut => sut.IsMinValue(decimal.MinValue, "Message"));
            AssertNotification(sut => sut.IsMinValue(decimal.MinValue, "number", "Message"), "number");
            AssertNotification(sut => sut.IsNotMinValue(0m, "Message"));
            AssertNotification(sut => sut.IsMaxValue(decimal.MaxValue, "Message"));
            AssertNotification(sut => sut.IsNotMaxValue(0m, "number", "Message"), "number");

            AssertNotification(sut => sut.AreEquals(5.5m, 5.5m, "Message"));
            AssertNotification(sut => sut.AreNotEquals(5.5m, 6.5m, "number", "Message"), "number");

            AssertNotification(sut => sut.IsBetween(7.5m, 5m, 10m, "Message"));
            AssertNotification(sut => sut.IsNotBetween(4.5m, 5m, 10m, "number", "Message"), "number");

            AssertNotification(sut => sut.Contains(5.5m, list, "Message"));
            AssertNotification(sut => sut.NotContains(9.5m, list, "number", "Message"), "number");
        }
    }

    public class DoubleValidationContractTests : ContractValidationTestBase
    {
        [Fact]
        public void WhenDoubleRulesAreEvaluatedThenCurrentBehaviorIsApplied()
        {
            var list = new[] { 4.5d, 5.5d, 6.5d };

            AssertNotification(sut => sut.IsNull((double?)null, "Message"));
            AssertNotification(sut => sut.IsNotNull((double?)1d, "number", "Message"), "number");

            AssertNotification(sut => sut.IsGreaterThan(5.5d, 4.5d, "Message"));
            AssertNotification(sut => sut.IsGreaterOrEqualsThan(5.5d, 5.5d, "number", "Message"), "number");
            AssertNotification(sut => sut.IsLowerThan(4.5d, 5.5d, "Message"));
            AssertNotification(sut => sut.IsLowerOrEqualsThan(5.5d, 5.5d, "number", "Message"), "number");

            AssertNotification(sut => sut.IsMinValue(double.MinValue, "Message"));
            AssertNotification(sut => sut.IsNotMinValue(0d, "number", "Message"), "number");
            AssertNotification(sut => sut.IsMaxValue(double.MaxValue, "Message"));
            AssertNotification(sut => sut.IsNotMaxValue(0d, "number", "Message"), "number");

            AssertNotification(sut => sut.AreEquals(5.5d, 5.5d, "Message"));
            AssertNotification(sut => sut.AreNotEquals(5.5d, 6.5d, "number", "Message"), "number");
            AssertNotification(sut => sut.IsBetween(7.5d, 5d, 10d, "Message"));
            AssertNotification(sut => sut.IsNotBetween(4.5d, 5d, 10d, "number", "Message"), "number");
            AssertNotification(sut => sut.Contains(5.5d, list, "Message"));
            AssertNotification(sut => sut.NotContains(9.5d, list, "number", "Message"), "number");
        }
    }

    public class FloatValidationContractTests : ContractValidationTestBase
    {
        [Fact]
        public void WhenFloatRulesAreEvaluatedThenCurrentBehaviorIsApplied()
        {
            var list = new[] { 4.5f, 5.5f, 6.5f };

            AssertNotification(sut => sut.IsNull((float?)null, "Message"));
            AssertNotification(sut => sut.IsNotNull((float?)1f, "number", "Message"), "number");

            AssertNotification(sut => sut.IsGreaterThan(5.5f, 4.5f, "Message"));
            AssertNotification(sut => sut.IsGreaterOrEqualsThan(5.5f, 5.5f, "number", "Message"), "number");
            AssertNotification(sut => sut.IsLowerThan(4.5f, 5.5f, "Message"));
            AssertNotification(sut => sut.IsLowerOrEqualsThan(5.5f, 5.5f, "number", "Message"), "number");

            AssertNotification(sut => sut.IsMinValue(float.MinValue, "Message"));
            AssertNotification(sut => sut.IsNotMinValue(0f, "number", "Message"), "number");
            AssertNotification(sut => sut.IsMaxValue(float.MaxValue, "Message"));
            AssertNotification(sut => sut.IsNotMaxValue(0f, "number", "Message"), "number");

            AssertNotification(sut => sut.AreEquals(5.5f, 5.5f, "Message"));
            AssertNotification(sut => sut.AreNotEquals(5.5f, 6.5f, "number", "Message"), "number");
            AssertNotification(sut => sut.IsBetween(7.5f, 5f, 10f, "Message"));
            AssertNotification(sut => sut.IsNotBetween(4.5f, 5f, 10f, "number", "Message"), "number");
            AssertNotification(sut => sut.Contains(5.5f, list, "Message"));
            AssertNotification(sut => sut.NotContains(9.5f, list, "number", "Message"), "number");
        }
    }
}