using System.Threading.Tasks;

namespace Wimc.Domain.Repositories
{
    public interface IMessageRepository
    {
        Task Publish<TMessageType>(TMessageType message);
    }
}