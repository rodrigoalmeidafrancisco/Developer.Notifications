using Developer.Notifications.TestProject.Helpers;
using Developer.Notifications.TestProject.TestDoubles;
using System.Collections.ObjectModel;
using System.Linq;

namespace Developer.Notifications.TestProject
{
    public class NotifiableTests
    {
        [Fact]
        public void WhenCreatedThenStartsValidAndWithoutNotifications()
        {
            var sut = new TestNotifiable();

            NotificationAssert.HasNoNotifications(sut);
        }

        [Fact]
        public void WhenAddNotificationWithMessageOnlyThenAddsNotificationWithNullKey()
        {
            var sut = new TestNotifiable();

            sut.AddNotification("Message");

            NotificationAssert.HasSingleNotification(sut, null, "Message");
        }

        [Fact]
        public void WhenAddNotificationWithKeyThenAddsNotificationWithProvidedKey()
        {
            var sut = new TestNotifiable();

            sut.AddNotification("field", "Message");

            NotificationAssert.HasSingleNotification(sut, "field", "Message");
        }

        [Fact]
        public void WhenAddNotificationWithTypeThenUsesTypeNameAsKey()
        {
            var sut = new TestNotifiable();

            sut.AddNotification(typeof(string), "Message");

            NotificationAssert.HasSingleNotification(sut, "String", "Message");
        }

        [Fact]
        public void WhenAddNotificationObjectThenAppendsProvidedInstance()
        {
            var sut = new TestNotifiable();
            var notification = new Notification("field", "Message");

            sut.AddNotification(notification);

            var storedNotification = Assert.Single(sut.Notifications);
            Assert.Same(notification, storedNotification);
        }

        [Fact]
        public void WhenSameMessageOnlyIsAddedTwiceThenBothNotificationsRemain()
        {
            var sut = new TestNotifiable();

            sut.AddNotification("Message");
            sut.AddNotification("Message");

            NotificationAssert.HasNotificationCount(sut, 2);
            Assert.All(sut.Notifications, notification =>
            {
                Assert.Null(notification.Key);
                Assert.Equal("Message", notification.Message);
            });
        }

        [Fact]
        public void WhenGetNotificationsMessagesDistinctIsCalledThenReturnsUniqueMessagesOnly()
        {
            var sut = new TestNotifiable();
            sut.AddNotification("name", "Repeated");
            sut.AddNotification("email", "Repeated");
            sut.AddNotification("document", "Another");

            var result = sut.GetNotificationsMessagesDistinct();

            Assert.Equal(2, result.Count);
            Assert.Equal(new[] { "Repeated", "Another" }, result);
        }

        [Fact]
        public void WhenAddNotificationsReceivesReadOnlyCollectionThenAppendsAllItems()
        {
            var sut = new TestNotifiable();
            IReadOnlyCollection<Notification> notifications = new ReadOnlyCollection<Notification>(new[]
            {
                new Notification("a", "Message A"),
                new Notification("b", "Message B")
            });

            sut.AddNotifications(notifications);

            NotificationAssert.HasNotificationCount(sut, 2);
        }

        [Fact]
        public void WhenAddNotificationsReceivesListThenAppendsAllItems()
        {
            var sut = new TestNotifiable();
            IList<Notification> notifications = new List<Notification>
            {
                new Notification("a", "Message A")
            };

            sut.AddNotifications(notifications);

            NotificationAssert.HasNotificationCount(sut, 1);
        }

        [Fact]
        public void WhenAddNotificationsReceivesCollectionThenAppendsAllItems()
        {
            var sut = new TestNotifiable();
            ICollection<Notification> notifications = new Collection<Notification>
            {
                new Notification("a", "Message A"),
                new Notification("b", "Message B"),
                new Notification("c", "Message C")
            };

            sut.AddNotifications(notifications);

            NotificationAssert.HasNotificationCount(sut, 3);
        }

        [Fact]
        public void WhenAddNotificationsReceivesOtherNotifiablesThenAppendsAllChildNotifications()
        {
            var sut = new TestNotifiable();
            var first = new TestNotifiable();
            var second = new TestNotifiable();

            first.AddNotification("a", "Message A");
            second.AddNotification("b", "Message B");
            second.AddNotification("c", "Message C");

            sut.AddNotifications(first, second);

            Assert.Equal(3, sut.Notifications.Count);
            Assert.Equal(new[] { "a", "b", "c" }, sut.Notifications.Select(notification => notification.Key));
        }

        [Fact]
        public void WhenClearIsCalledThenRemovesNotificationsAndRestoresValidity()
        {
            var sut = new TestNotifiable();
            sut.AddNotification("Message");

            sut.Clear();

            NotificationAssert.HasNoNotifications(sut);
        }
    }
}