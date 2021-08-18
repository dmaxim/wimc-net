using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Rebus.Bus;
using Wimc.Domain.Clients;

namespace Wimc.Data.Clients
{
    public class MessageClient : IMessageClient
    {
        private readonly string _messageBusConnectionString;
        private readonly IBus _messageBus;
        public MessageClient(string connectionString, IBus messageBus)
        {
            _messageBusConnectionString = connectionString;
            _messageBus = messageBus;
        }
        public async Task Publish<TMessageType>(IList<TMessageType> messages)
        {
            await PublishViaRebus(messages).ConfigureAwait(false);
        }

        private async Task PublishViaRebus<TMessageType>(IList<TMessageType> messages)
        {
            foreach (var message in messages)
            {
                await _messageBus.Publish(message).ConfigureAwait(false);
            }
        }
        
        public async Task Receive(string topicName, string subscriptionName)
        {
            var client = new ServiceBusClient(_messageBusConnectionString);
            var processor = client.CreateProcessor(topicName, subscriptionName, new ServiceBusProcessorOptions()
            {
                ReceiveMode = ServiceBusReceiveMode.PeekLock
            });

            try
            {
                processor.ProcessMessageAsync += MessageHandler;
                processor.ProcessErrorAsync += ErrorHandler;
                await processor.StartProcessingAsync().ConfigureAwait(false);

                Thread.Sleep(10000);
                
                await processor.StopProcessingAsync().ConfigureAwait(false);
            }
            finally
            {
                await processor.DisposeAsync();
                await client.DisposeAsync();
            }
        }

        private async Task MessageHandler(ProcessMessageEventArgs args)
        {
            await args.CompleteMessageAsync(args.Message).ConfigureAwait(false);
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            return Task.CompletedTask;
        }
        
    }
}