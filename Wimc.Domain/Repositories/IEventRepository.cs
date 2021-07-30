using System.Collections.Generic;
using System.Threading.Tasks;

namespace Wimc.Domain.Repositories
{
    public interface IEventRepository
    {
        Task Publish<TEventType>(IList<TEventType> messages);
    }
}