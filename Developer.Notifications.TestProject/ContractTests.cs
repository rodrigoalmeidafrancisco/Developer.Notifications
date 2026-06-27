using Developer.Notifications.TestProject.Helpers;
using Developer.Notifications.TestProject.TestDoubles;
using Developer.Notifications.Validations;

namespace Developer.Notifications.TestProject
{
    public class ContractTests
    {
        [Fact]
        public void WhenRequiresIsCalledThenReturnsSameInstanceWithoutChangingState()
        {
            var sut = new Contract<object>();

            var result = sut.Requires();

            Assert.Same(sut, result);
            NotificationAssert.HasNoNotifications(sut);
        }

        [Fact]
        public void WhenJoinReceivesNullThenKeepsStateUnchanged()
        {
            var sut = new Contract<object>();

            sut.Join(null);

            NotificationAssert.HasNoNotifications(sut);
        }

        [Fact]
        public void WhenJoinReceivesValidAndInvalidNotifiablesThenCopiesOnlyInvalidNotifications()
        {
            var sut = new Contract<object>();
            var valid = new TestNotifiable();
            var invalidFirst = new TestNotifiable();
            var invalidSecond = new TestNotifiable();

            invalidFirst.AddNotification("a", "Message A");
            invalidSecond.AddNotification("b", "Message B");
            invalidSecond.AddNotification("c", "Message C");

            sut.Join(valid, invalidFirst, invalidSecond);

            Assert.Equal(3, sut.Notifications.Count);
            Assert.Equal(new[] { "a", "b", "c" }, sut.Notifications.Select(notification => notification.Key));
        }
    }
}