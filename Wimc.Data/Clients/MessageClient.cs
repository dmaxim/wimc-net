using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Mx.Library.ExceptionHandling;
using Mx.Library.Serialization;
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
        public async Task Publish<TMessageType>(IList<TMessageType> messages, string topicName)
        {
            await PublishViaRebus(messages, topicName).ConfigureAwait(false);
            /*
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
            */
            

        }

        private async Task PublishViaRebus<TMessageType>(IList<TMessageType> messages, string topicName)
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
            var messageBody = args.Message.Body.ToString();
            await args.CompleteMessageAsync(args.Message).ConfigureAwait(false);
        }

        private Task ErrorHandler(ProcessErrorEventArgs args)
        {
            var error = args.Exception.ToString();
            return Task.CompletedTask;
            
        }
        
    }
}