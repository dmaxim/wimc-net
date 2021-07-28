using System.Threading.Tasks;
using Wimc.Domain.Repositories;

namespace Wimc.Data.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        public Task Publish<TMessageType>(TMessageType message)
        {
            return Task.FromResult(0);
        }
    }
}