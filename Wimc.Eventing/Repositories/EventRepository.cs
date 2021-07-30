using System.Collections.Generic;
using System.Threading.Tasks;
using Wimc.Domain.Repositories;

namespace Wimc.Eventing.Repositories
{
    public class EventRepository : IEventRepository
    {
        public Task Publish<TEventType>(IList<TEventType> messages)
        {
            throw new System.NotImplementedException();
        }
    }
}