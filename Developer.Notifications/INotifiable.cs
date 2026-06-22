using System.Collections.Generic;

namespace Developer.Notifications
{
    public interface INotifiable
    {
        void AddNotifications(IEnumerable<Notification> notifications);
    }
}
