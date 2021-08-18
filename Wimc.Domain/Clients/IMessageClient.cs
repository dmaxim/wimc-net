using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wimc.Domain.Clients
{
    public interface IMessageClient
    {
        Task Publish<TMessageType>(IList<TMessageType> messages);

        Task Receive(string topicName, string subscriptionName);
    }
}