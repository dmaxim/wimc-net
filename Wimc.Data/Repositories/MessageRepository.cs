using System.Collections.Generic;
using System.Threading.Tasks;
using Wimc.Domain.Clients;
using Wimc.Domain.Repositories;

namespace Wimc.Data.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly IMessageClient _messageClient;
        
        public MessageRepository(IMessageClient messageClient)
        {
            _messageClient = messageClient;
        }
        
        public async Task Publish<TMessageType>(IList<TMessageType> messages)
        {
            await _messageClient.Publish(messages).ConfigureAwait(false);
        }
    }
}