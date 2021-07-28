using System.Collections.Generic;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Mx.Library.ExceptionHandling;
using Mx.Library.Serialization;
using Wimc.Domain.Clients;

namespace Wimc.Data.Clients
{
    public class MessageClient : IMessageClient
    {
        private readonly string _messageBusConnectionString;
        public MessageClient(string connectionString)
        {
            _messageBusConnectionString = connectionString;
        }
        public async Task Publish<TMessageType>(IList<TMessageType> messages, string topicName)
        {
            var client = new ServiceBusClient(_messageBusConnectionString);
            var sender = client.CreateSender(topicName);
            using var messageBatch = await sender.CreateMessageBatchAsync();

            foreach (var message in messages)
            {
                if(!messageBatch.TryAddMessage(new ServiceBusMessage(message.ToJson())))
                {
                    throw new MxApplicationStateException("Unable to add message");
                }    
            }

            if (messageBatch.Count > 0)
            {
                try
                {
                    await sender.SendMessagesAsync(messageBatch).ConfigureAwait(false);
                }
                finally
                {
                    await sender.DisposeAsync().ConfigureAwait(false);
                    await client.DisposeAsync().ConfigureAwait(false);
                }    
            }
            else
            {
                throw new MxApplicationStateException("No messages to publish");
            }
            

        }
    }
}