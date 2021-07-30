using System.Collections.Generic;
using System.Threading.Tasks;
using Wimc.Domain.Messages.Events;

namespace Wimc.Domain.Repositories
{
    public interface IEventRepository
    {
        Task Publish<TEventType>(IList<TEventType> events) where TEventType : WimcEvent;
    }
}