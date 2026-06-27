namespace Developer.Notifications
{
    public class Notification
    {
        public Notification()
        {

        }

        public Notification(string message)
        {
            Key = null;
            Message = message;
        }

        public Notification(string key, string message)
        {
            Key = key;
            Message = message;
        }

        public string Key { get; set; }
        public string Message { get; set; }
    }
}
