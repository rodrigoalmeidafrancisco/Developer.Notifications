namespace Developer.Notifications.TestProject
{
    public class NotificationTests
    {
        [Fact]
        public void WhenCreatedWithDefaultConstructorThenKeyAndMessageAreNull()
        {
            var sut = new Notification();

            Assert.Null(sut.Key);
            Assert.Null(sut.Message);
        }

        [Fact]
        public void WhenCreatedWithMessageOnlyThenStoresNullKeyAndProvidedMessage()
        {
            var sut = new Notification("Message");

            Assert.Null(sut.Key);
            Assert.Equal("Message", sut.Message);
        }

        [Fact]
        public void WhenCreatedWithKeyAndMessageThenStoresBothValues()
        {
            var sut = new Notification("field", "Message");

            Assert.Equal("field", sut.Key);
            Assert.Equal("Message", sut.Message);
        }
    }
}