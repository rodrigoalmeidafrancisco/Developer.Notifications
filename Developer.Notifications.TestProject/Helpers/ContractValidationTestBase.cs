using Developer.Notifications.Validations;

namespace Developer.Notifications.TestProject.Helpers
{
    public abstract class ContractValidationTestBase
    {
        protected static Contract<object> Execute(Action<Contract<object>> action)
        {
            var sut = new Contract<object>();
            action(sut);
            return sut;
        }

        protected static void AssertNotification(Action<Contract<object>> action, string expectedKey = null, string expectedMessage = "Message")
        {
            var sut = Execute(action);
            NotificationAssert.HasSingleNotification(sut, expectedKey, expectedMessage);
        }

        protected static void AssertNoNotifications(Action<Contract<object>> action)
        {
            var sut = Execute(action);
            NotificationAssert.HasNoNotifications(sut);
        }
    }
}