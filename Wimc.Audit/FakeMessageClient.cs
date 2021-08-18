using System.Collections.Generic;
using System.Threading.Tasks;
using Wimc.Domain.Clients;

namespace Wimc.Audit
{
    public class FakeMessageClient : IMessageClient
    {
        public Task Publish<TMessageType>(IList<TMessageType> messages)
        {
            throw new System.NotImplementedException();
        }

        public Task Receive(string topicName, string subscriptionName)
        {
            throw new System.NotImplementedException();
        }
    }
}