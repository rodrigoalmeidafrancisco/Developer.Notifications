using System.Linq;

namespace Developer.Notifications.TestProject.Helpers
{
    internal static class NotificationAssert
    {
        public static void HasNoNotifications(Notifiable<Notification> notifiable)
        {
            Assert.True(notifiable.IsValid);
            Assert.Empty(notifiable.Notifications);
        }

        public static void HasSingleNotification(Notifiable<Notification> notifiable, string expectedKey, string expectedMessage)
        {
            var notification = Assert.Single(notifiable.Notifications);

            Assert.False(notifiable.IsValid);
            Assert.Equal(expectedKey, notification.Key);
            Assert.Equal(expectedMessage, notification.Message);
        }

        public static void HasNotificationCount(Notifiable<Notification> notifiable, int expectedCount)
        {
            Assert.Equal(expectedCount, notifiable.Notifications.Count);
            Assert.Equal(expectedCount == 0, notifiable.IsValid);
        }
    }
}