namespace Cqrs.Infra.CrossCutting.Configuration
{
    public class RabbitMQSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string TopicNewAlertPrice { get; set; }
        public string QueueCreateNewAlertPrice { get; set; }
        public bool Active { get; set; }
    }
}
