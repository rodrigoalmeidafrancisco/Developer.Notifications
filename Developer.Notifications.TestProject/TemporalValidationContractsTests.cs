using Developer.Notifications.TestProject.Helpers;

namespace Developer.Notifications.TestProject
{
    public class DateTimeValidationContractTests : ContractValidationTestBase
    {
        private static readonly DateTime _firstDate = new DateTime(2024, 1, 1);
        private static readonly DateTime _secondDate = new DateTime(2024, 1, 2);
        private static readonly DateTime _thirdDate = new DateTime(2024, 1, 3);
        private static readonly DateTime _outsideDate = new DateTime(2023, 12, 31);

        [Fact]
        public void WhenDateTimeRulesAreEvaluatedThenCurrentBehaviorIsApplied()
        {
            var list = new[] { _firstDate, _secondDate, _thirdDate };

            AssertNotification(sut => sut.IsNull((DateTime?)null, "Message"));
            AssertNotification(sut => sut.IsNull((DateTime?)null, "date", "Message"), "date");
            AssertNoNotifications(sut => sut.IsNull((DateTime?)_firstDate, "Message"));

            AssertNotification(sut => sut.IsNotNull((DateTime?)_firstDate, "Message"));
            AssertNotification(sut => sut.IsNotNull((DateTime?)_firstDate, "date", "Message"), "date");
            AssertNoNotifications(sut => sut.IsNotNull((DateTime?)null, "Message"));

            AssertNotification(sut => sut.IsGreaterThan(_secondDate, _firstDate, "Message"));
            AssertNotification(sut => sut.IsGreaterThan(_secondDate, _firstDate, "date", "Message"), "date");
            AssertNoNotifications(sut => sut.IsGreaterThan(_firstDate, _secondDate, "Message"));

            AssertNotification(sut => sut.IsGreaterOrEqualsThan(_secondDate, _secondDate, "Message"));
            AssertNotification(sut => sut.IsLowerThan(_firstDate, _secondDate, "date", "Message"), "date");
            AssertNotification(sut => sut.IsLowerOrEqualsThan(_secondDate, _secondDate, "Message"));

            AssertNotification(sut => sut.IsMinValue(DateTime.MinValue, "Message"));
            AssertNotification(sut => sut.IsMinValue(DateTime.MinValue, "date", "Message"), "date");
            AssertNotification(sut => sut.IsNotMinValue(_firstDate, "Message"));
            AssertNotification(sut => sut.IsMaxValue(DateTime.MaxValue, "date", "Message"), "date");
            AssertNotification(sut => sut.IsNotMaxValue(_firstDate, "Message"));

            AssertNotification(sut => sut.AreEquals(_secondDate, _secondDate, "Message"));
            AssertNotification(sut => sut.AreNotEquals(_secondDate, _thirdDate, "date", "Message"), "date");

            AssertNotification(sut => sut.IsBetween(_secondDate, _firstDate, _thirdDate, "Message"));
            AssertNotification(sut => sut.IsBetween(_secondDate, _firstDate, _thirdDate, "date", "Message"), "date");
            AssertNoNotifications(sut => sut.IsBetween(_outsideDate, _firstDate, _thirdDate, "Message"));

            AssertNotification(sut => sut.IsNotBetween(_outsideDate, _firstDate, _thirdDate, "Message"));
            AssertNotification(sut => sut.IsNotBetween(_outsideDate, _firstDate, _thirdDate, "date", "Message"), "date");
            AssertNoNotifications(sut => sut.IsNotBetween(_secondDate, _firstDate, _thirdDate, "Message"));

            AssertNotification(sut => sut.Contains(_secondDate, list, "Message"));
            AssertNotification(sut => sut.Contains(_secondDate, list, "date", "Message"), "date");
            AssertNoNotifications(sut => sut.Contains(_outsideDate, list, "Message"));

            AssertNotification(sut => sut.NotContains(_outsideDate, list, "Message"));
            AssertNotification(sut => sut.NotContains(_outsideDate, list, "date", "Message"), "date");
            AssertNoNotifications(sut => sut.NotContains(_secondDate, list, "Message"));
        }
    }

    public class TimeSpanValidationContractTests : ContractValidationTestBase
    {
        private static readonly TimeSpan _firstSpan = TimeSpan.FromHours(1);
        private static readonly TimeSpan _secondSpan = TimeSpan.FromHours(2);
        private static readonly TimeSpan _thirdSpan = TimeSpan.FromHours(3);
        private static readonly TimeSpan _outsideSpan = TimeSpan.FromMinutes(30);

        [Fact]
        public void WhenTimeSpanRulesAreEvaluatedThenCurrentBehaviorIsApplied()
        {
            var list = new[] { _firstSpan, _secondSpan, _thirdSpan };

            AssertNotification(sut => sut.IsNull((TimeSpan?)null, "Message"));
            AssertNotification(sut => sut.IsNull((TimeSpan?)null, "time", "Message"), "time");
            AssertNoNotifications(sut => sut.IsNull((TimeSpan?)_firstSpan, "Message"));

            AssertNotification(sut => sut.IsNotNull((TimeSpan?)_firstSpan, "Message"));
            AssertNotification(sut => sut.IsNotNull((TimeSpan?)_firstSpan, "time", "Message"), "time");
            AssertNoNotifications(sut => sut.IsNotNull((TimeSpan?)null, "Message"));

            AssertNotification(sut => sut.IsGreaterThan(_secondSpan, _firstSpan, "Message"));
            AssertNotification(sut => sut.IsGreaterThan(_secondSpan, _firstSpan, "time", "Message"), "time");
            AssertNoNotifications(sut => sut.IsGreaterThan(_firstSpan, _secondSpan, "Message"));

            AssertNotification(sut => sut.IsGreaterOrEqualsThan(_secondSpan, _secondSpan, "Message"));
            AssertNotification(sut => sut.IsLowerThan(_firstSpan, _secondSpan, "time", "Message"), "time");
            AssertNotification(sut => sut.IsLowerOrEqualsThan(_secondSpan, _secondSpan, "Message"));

            AssertNotification(sut => sut.IsMinValue(TimeSpan.MinValue, "Message"));
            AssertNotification(sut => sut.IsMinValue(TimeSpan.MinValue, "time", "Message"), "time");
            AssertNotification(sut => sut.IsNotMinValue(_firstSpan, "Message"));
            AssertNotification(sut => sut.IsMaxValue(TimeSpan.MaxValue, "time", "Message"), "time");
            AssertNotification(sut => sut.IsNotMaxValue(_firstSpan, "Message"));

            AssertNotification(sut => sut.AreEquals(_secondSpan, _secondSpan, "Message"));
            AssertNotification(sut => sut.AreNotEquals(_secondSpan, _thirdSpan, "time", "Message"), "time");

            AssertNotification(sut => sut.IsBetween(_secondSpan, _firstSpan, _thirdSpan, "Message"));
            AssertNotification(sut => sut.IsBetween(_secondSpan, _firstSpan, _thirdSpan, "time", "Message"), "time");
            AssertNoNotifications(sut => sut.IsBetween(_outsideSpan, _firstSpan, _thirdSpan, "Message"));

            AssertNotification(sut => sut.IsNotBetween(_outsideSpan, _firstSpan, _thirdSpan, "Message"));
            AssertNotification(sut => sut.IsNotBetween(_outsideSpan, _firstSpan, _thirdSpan, "time", "Message"), "time");
            AssertNoNotifications(sut => sut.IsNotBetween(_secondSpan, _firstSpan, _thirdSpan, "Message"));

            AssertNotification(sut => sut.Contains(_secondSpan, list, "Message"));
            AssertNotification(sut => sut.Contains(_secondSpan, list, "time", "Message"), "time");
            AssertNoNotifications(sut => sut.Contains(_outsideSpan, list, "Message"));

            AssertNotification(sut => sut.NotContains(_outsideSpan, list, "Message"));
            AssertNotification(sut => sut.NotContains(_outsideSpan, list, "time", "Message"), "time");
            AssertNoNotifications(sut => sut.NotContains(_secondSpan, list, "Message"));
        }
    }
}