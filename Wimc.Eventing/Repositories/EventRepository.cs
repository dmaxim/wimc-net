using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Azure.Messaging.EventGrid;
using Mx.Library.Serialization;
using Wimc.Domain.Messages.Events;
using Wimc.Domain.Repositories;
using Wimc.Eventing.Configuration;

namespace Wimc.Eventing.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly EventClientConfiguration _eventClientConfiguration;

        public EventRepository(EventClientConfiguration eventClientConfiguration)
        {
            _eventClientConfiguration = eventClientConfiguration;
        }
        
        public async Task Publish<TEventType>(IList<TEventType> events) where TEventType : WimcEvent
        {
         
            
            var client = new EventGridPublisherClient(new Uri(_eventClientConfiguration.Endpoint), new AzureKeyCredential(_eventClientConfiguration.Key));

            var gridEvents = events.Select(eventType => new EventGridEvent(eventType.Subject, eventType.GetType().FullName, "1.0", eventType.ToJson()) {Topic = eventType.Topic}).ToList();

            if (gridEvents.Any())
            {
                await client.SendEventsAsync(gridEvents).ConfigureAwait(false);
            }


        }
    }
}