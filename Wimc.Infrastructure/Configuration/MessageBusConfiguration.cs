namespace Wimc.Infrastructure.Configuration
{
    public class MessageBusConfiguration
    {
        public string ConnectionString { get; set; }
        
        public string QueueName { get; set; }
    }
}